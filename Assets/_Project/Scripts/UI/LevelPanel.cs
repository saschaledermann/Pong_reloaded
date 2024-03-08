using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LevelPanel : Panel
{
    [SerializeField] PaddleSettings[] m_paddleSettings;
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

        for (var i = 0; i < m_levelButtons.Length; i++)
        {
            var index = i;
            m_levelButtons[i].onClick.AddListener(() => 
            {
                if (SettingsManager.Instance != null)
                    SettingsManager.Instance.opponentSettings = m_paddleSettings[index];
                SceneManager.LoadScene(1);
            });
            m_levelButtons[i].interactable = i < level;
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
