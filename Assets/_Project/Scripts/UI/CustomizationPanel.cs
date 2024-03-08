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
        int boosters;
        if (PlayerPrefs.HasKey("boosters"))
            boosters = PlayerPrefs.GetInt("boosters");
        else
            boosters = 0;

        m_backButton.onClick.AddListener(() =>
        {
            if (canvasController == null) return;
            canvasController.OpenPanel<MainPanel>(this, new Vector2(1350, -325));
        });

        m_powerUpToggle1.isOn = false;
        m_powerUpToggle1.onValueChanged.AddListener(value =>
        {
            if (value)
                SettingsManager.Instance.PlayerSettings.Boost = Boost.Angle;
            else
                SettingsManager.Instance.PlayerSettings.Boost = Boost.None;
        });
        m_powerUpToggle1.interactable = boosters > 0;

        m_powerUpToggle2.isOn = false;
        m_powerUpToggle2.onValueChanged.AddListener(value =>
        {
            if (value)
                SettingsManager.Instance.PlayerSettings.Boost = Boost.Stun;
            else
                SettingsManager.Instance.PlayerSettings.Boost = Boost.None;
        });
        m_powerUpToggle2.interactable = boosters > 1;

        m_powerUpToggle3.isOn = false;
        m_powerUpToggle3.onValueChanged.AddListener(value =>
        {
            if (value)
                SettingsManager.Instance.PlayerSettings.Boost = Boost.Power;
            else
                SettingsManager.Instance.PlayerSettings.Boost = Boost.None;
        });
        m_powerUpToggle3.interactable = boosters > 2;

    }

    public override void SetInteractables(bool state)
    {
        int boosters;
        if (PlayerPrefs.HasKey("boosters"))
            boosters = PlayerPrefs.GetInt("boosters");
        else
            boosters = 0;
        
        m_powerUpToggle1.interactable = state && boosters > 0;
        m_powerUpToggle2.interactable = state && boosters > 1;
        m_powerUpToggle3.interactable = state && boosters > 2;
        m_backButton.interactable = state;
    }
}
