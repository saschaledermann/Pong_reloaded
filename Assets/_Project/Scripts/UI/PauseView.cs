using System.Threading.Tasks;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PauseView : MonoBehaviour
{
    [SerializeField] Button m_resumeButton;
    [SerializeField] Button m_restartButton;
    [SerializeField] Button m_quitButton;
    [SerializeField] Button m_pauseButton;
    Vector2 m_onPosition = Vector2.zero;
    Vector2 m_offPosition = new(0, 1600);

    void Start()
    {
        GetComponent<RectTransform>().localPosition = m_offPosition;

        if (GameManager.Instance != null)
        {
            m_resumeButton.onClick.AddListener(() =>
            {
                GameManager.Instance.TogglePause();
                m_pauseButton.transform.GetChild(0).GetComponent<TMP_Text>().text = GameManager.Instance.Paused ? "|>" : "||";
                TogglePause();
                m_pauseButton.interactable = true;
            });
        
            m_restartButton.onClick.AddListener(async () => 
            {
                await MoveView(false);
                GameManager.Instance.RestartGame();
                GameManager.Instance.TogglePause();
                m_pauseButton.transform.GetChild(0).GetComponent<TMP_Text>().text = GameManager.Instance.Paused ? "|>" : "||";
                m_pauseButton.interactable = true;
            });
            
            m_quitButton.onClick.AddListener(() => 
            {
                GameManager.Instance.TogglePause();
                SceneManager.LoadScene("Main");
            });
        }
    }

    public void TogglePause()
    {
        if (GameManager.Instance == null) return;

        var paused = GameManager.Instance.Paused;
        _ = MoveView(paused);
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
        m_resumeButton.interactable = value;
        m_restartButton.interactable = value;
        m_quitButton.interactable = value;
    }
}
