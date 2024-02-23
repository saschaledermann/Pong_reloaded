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
    Vector2 m_offPosition = new(0, 1600);

    void Start()
    {
        if (GameManager.Instance != null)
        {
            GameManager.Instance.gameOver += value => EndGame(value);
        }

        m_title.text = "-";
        m_subTitle.text = string.Empty;
        UnlockString = string.Empty;

        m_nextLevelButton.interactable = false;

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
        m_title.text = playerWon ? "You won!" : "You lost!";
        m_subTitle.text = string.IsNullOrEmpty(UnlockString) ? $"{UnlockString} unlocked!" : string.Empty;
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
        // m_nextLevelButton.interactable = value;
        m_restartButton.interactable = value;
        m_quitButton.interactable = value;
    }
}