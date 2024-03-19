using UnityEngine;
using UnityEngine.UI;

public class AboutPanel : Panel
{
    [SerializeField] Button m_backButton;

    void Start()
    {
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
    }
    public override void SetInteractables(bool state) => m_backButton.interactable = state;
}
