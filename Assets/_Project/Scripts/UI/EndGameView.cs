using System.Threading.Tasks;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class EndGameView : MonoBehaviour
{
    public string UnlockString { get; set; }
    [SerializeField] TMP_Text m_title;
    [SerializeField] TMP_Text m_subTitle;
    [SerializeField] Button m_restartButton;
    [SerializeField] Button m_nextLevelButton;
    [SerializeField] Button m_quitButton;
    [SerializeField] Button m_pauseButton;
    Vector2 m_onPosition = Vector2.zero;
    Vector2 m_offPosition = new(0, 2500);

    void Start()
    {
        if (GameManager.Instance != null)
        {
            GameManager.Instance.gameOver += value => EndGame(value);
        }

        m_title.text = "-";
        m_subTitle.text = string.Empty;
        UnlockString = SettingsManager.Instance.OpponentSettings.UnlockString;

        m_nextLevelButton.onClick.AddListener(() =>
        {
            SceneManager.LoadScene("Level");
        });
        m_nextLevelButton.interactable = SettingsManager.Instance.Level < 9;

        m_restartButton.onClick.AddListener(async () =>
        {
            SetInteractables(false);
            await MoveView(false);
            GameManager.Instance.RestartGame();
            GameManager.Instance.TogglePause();
            m_pauseButton.transform.GetChild(0).GetComponent<TMP_Text>().text = GameManager.Instance.Paused ? "|>" : "||";
            m_pauseButton.interactable = true;
        });

        m_quitButton.onClick.AddListener(() =>
        {
            GameManager.Instance.TogglePause();
            SceneManager.LoadScene(0);
        });
    }

    public async void EndGame(bool playerWon)
    {
        m_subTitle.text = string.Empty;

        if (playerWon)
        {
            m_title.text = "You won!";

            if (PlayerPrefs.GetInt("Boosters") < SettingsManager.Instance.OpponentSettings.UnlockNumber &&
                !string.IsNullOrEmpty(UnlockString))
            {
                m_subTitle.text = $"{UnlockString} unlocked!";
                var boosterIndex = PlayerPrefs.GetInt("Boosters") + 1;
                PlayerPrefs.SetInt("Boosters", boosterIndex);
            }

            if (SettingsManager.Instance.Level < 9)
                SettingsManager.Instance.Level++;

            var level = SettingsManager.Instance.Level;
            if (level > PlayerPrefs.GetInt("Level"))
                PlayerPrefs.SetInt("Level", level);
        }
        else
        {
            m_title.text = "You lost!";
        }

        m_nextLevelButton.gameObject.SetActive(playerWon);
        m_restartButton.gameObject.SetActive(!playerWon);

        if (GameManager.Instance != null)
        {
            GameManager.Instance.TogglePause();
            await MoveView(true);
            SetInteractables(true);
        }
    }

    async Task MoveView(bool value, float duration = 0.25f)
    {
        // Disable caller interactables
        SetInteractables(value);

        var rectTransform = GetComponent<RectTransform>();
        var destination = value ? m_onPosition : m_offPosition;
        var startPos = !value ? m_onPosition : m_offPosition;
        float elapsed = 0f;

        while (elapsed < duration)
        {
            elapsed += Time.unscaledDeltaTime;
            var t = elapsed / duration;

            rectTransform.localPosition = Vector2.Lerp(startPos, destination, t);

            await Task.Yield();
        }

        rectTransform.localPosition = destination;
    }

    void SetInteractables(bool value)
    {
        m_nextLevelButton.interactable = value;
        m_restartButton.interactable = value;
        m_quitButton.interactable = value;
    }
}
