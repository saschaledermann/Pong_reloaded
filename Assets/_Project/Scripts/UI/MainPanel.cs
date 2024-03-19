using UnityEngine;
using UnityEngine.UI;

public class MainPanel : Panel
{
    [SerializeField] Button m_startButton;
    [SerializeField] Button m_customiseButton;
    [SerializeField] Button m_quitButton;
    [SerializeField] Button m_aboutButton;
    [SerializeField] Toggle m_audioToggle;

    void Start()
    {
        // Add button callbacks
        m_startButton.onClick.AddListener(() => OpenPanel<LevelPanel>());
        m_startButton.onClick.AddListener(() =>
        {
            if (AudioManager.Instance == null) return;
            AudioManager.Instance.PlayUiClip();
        });
        m_customiseButton.onClick.AddListener(() => OpenPanel<CustomizationPanel>());
        m_customiseButton.onClick.AddListener(() =>
        {
            if (AudioManager.Instance == null) return;
            AudioManager.Instance.PlayUiClip();
        });
        m_quitButton.onClick.AddListener(() => Quit());
        m_aboutButton.onClick.AddListener(() => OpenPanel<AboutPanel>());
        m_aboutButton.onClick.AddListener(() =>
        {
            if (AudioManager.Instance == null) return;
            AudioManager.Instance.PlayUiClip();
        });

        if (AudioManager.Instance != null)
            m_audioToggle.isOn = AudioManager.Instance.Paused;
        else
            m_audioToggle.interactable = false;
        
        m_audioToggle.onValueChanged.AddListener(value => 
        {
            if (AudioManager.Instance != null)
                AudioManager.Instance.ToggleAudio(value);
        });
        m_audioToggle.onValueChanged.AddListener(_ =>
        {
            if (AudioManager.Instance == null) return;
            AudioManager.Instance.PlayUiClip();
        });
    }


    public override void SetInteractables(bool state)
    {
        m_startButton.interactable = state;
        m_customiseButton.interactable = state;
        m_quitButton.interactable = state;
        m_aboutButton.interactable = state;
    }

    void OpenPanel<T>() where T : Panel
    {
        if (canvasController == null) return;

        canvasController.OpenPanel<T>(this, new Vector2(-1350, -325));
    }

    void Quit()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#endif
        Application.Quit();
    }
}
