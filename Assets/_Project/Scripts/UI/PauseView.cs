using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PauseView : MonoBehaviour
{
    [SerializeField] Button m_resumeButton;
    [SerializeField] Button m_restartButton;
    [SerializeField] Button m_quitButton;
    [SerializeField] Toggle m_pauseToggle;
    Vector2 m_onPosition = Vector2.zero;
    Vector2 m_offPosition = new(0, 2500);

    void Start()
    {
        GetComponent<RectTransform>().localPosition = m_offPosition;

        if (GameManager.Instance != null)
        {
            m_resumeButton.onClick.AddListener(() =>
            {
                m_pauseToggle.isOn = false;
                m_pauseToggle.interactable = true;
            });
        
            m_restartButton.onClick.AddListener(() => 
            {
                GameManager.Instance.RestartGame();
                m_pauseToggle.isOn = false;
            });
            
            m_quitButton.onClick.AddListener(() => 
            {
                GameManager.Instance.TogglePause();
                SceneManager.LoadScene("Main");
            });
        }

        m_resumeButton.onClick.AddListener(() =>
        {
            if (AudioManager.Instance == null) return;
            AudioManager.Instance.PlayUiClip();
        });

        m_restartButton.onClick.AddListener(() =>
        {
            if (AudioManager.Instance == null) return;
            AudioManager.Instance.PlayUiClip();
        });

        m_quitButton.onClick.AddListener(() =>
        {
            if (AudioManager.Instance == null) return;
            AudioManager.Instance.PlayUiClip();
        });
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
