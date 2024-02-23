using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LevelPanel : Panel
{
    [SerializeField] Button[] m_levelButtons;
    [SerializeField] Button m_backButton;

    void Start()
    {
        m_backButton.onClick.AddListener(() =>
        {
            if (canvasController == null) return;
            canvasController.OpenPanel<MainPanel>(this, new Vector2(1350, -325));
        });

        foreach (var button in m_levelButtons)
        {
            button.onClick.AddListener(() => SceneManager.LoadScene(1));
        }
    }
    
    public override void SetInteractables(bool state)
    {
        foreach (var button in m_levelButtons)
            button.interactable = state;
        
        m_backButton.interactable = state;
    }
}
