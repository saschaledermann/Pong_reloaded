using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LevelPanel : Panel
{
    [SerializeField] Button[] m_levelButtons;
    [SerializeField] Button m_backButton;

    void Start()
    {
        int level;
        if (PlayerPrefs.HasKey("Level"))
            level = PlayerPrefs.GetInt("Level");
        else
            level = 1;

        m_backButton.onClick.AddListener(() =>
        {
            if (canvasController == null) return;
            canvasController.OpenPanel<MainPanel>(this, new Vector2(1350, -325));
        });
        m_backButton.onClick.AddListener(() =>
        {
            if (AudioManager.Instance == null) return;
            AudioManager.Instance.PlayUiClip();
        });

        for (var i = 0; i < m_levelButtons.Length; i++)
        {
            var index = i + 1;
            m_levelButtons[i].onClick.AddListener(() =>
            {
                Debug.Log($"Setting Level to: {index}");
                SettingsManager.Instance.Level = index;
                SceneManager.LoadScene("Level");
            });
            m_levelButtons[i].interactable = i < level;
            m_levelButtons[i].onClick.AddListener(() =>
            {
                if (AudioManager.Instance == null) return;
                AudioManager.Instance.PlayUiClip();
            });
        }
    }

    public override void SetInteractables(bool state)
    {
        int level;
        if (PlayerPrefs.HasKey("Level"))
            level = PlayerPrefs.GetInt("Level");
        else
            level = 1;

        for (int i = 0; i < level; i++)
            m_levelButtons[i].interactable = state;

        m_backButton.interactable = state;
    }
}
