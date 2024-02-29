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
        }
    }
    
    public override void SetInteractables(bool state)
    {
        foreach (var button in m_levelButtons)
            button.interactable = state;
        
        m_backButton.interactable = state;
    }
}
