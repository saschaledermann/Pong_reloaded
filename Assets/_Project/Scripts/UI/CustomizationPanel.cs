using UnityEngine;
using UnityEngine.UI;

public class CustomizationPanel : Panel
{
    [SerializeField] Toggle m_powerUpToggle1;
    [SerializeField] Toggle m_powerUpToggle2;
    [SerializeField] Toggle m_powerUpToggle3;
    [SerializeField] Button m_backButton;

    void Start()
    {
        m_backButton.onClick.AddListener(() =>
        {
            if (canvasController == null) return;
            canvasController.OpenPanel<MainPanel>(this, new Vector2(1350, -325));
        });
    }

    public override void SetInteractables(bool state)
    {
        m_powerUpToggle1.interactable = state;
        m_powerUpToggle2.interactable = state;
        m_powerUpToggle3.interactable = state;
        m_backButton.interactable = state;
    }
}
