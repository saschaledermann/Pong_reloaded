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
        if (PlayerPrefs.HasKey("Boosters"))
            boosters = PlayerPrefs.GetInt("Boosters");
        else
            boosters = 0;
        
        int currentBooster;
        if (PlayerPrefs.HasKey("Selected-Booster"))
            currentBooster = PlayerPrefs.GetInt("Selected-Booster");
        else
            currentBooster = 0;

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

        m_powerUpToggle1.onValueChanged.AddListener(value =>
        {
            if (value)
            {
                SettingsManager.Instance.PlayerSettings.Boost = Boost.Angle;
                PlayerPrefs.SetInt("Selected-Booster", 1);
            }
            else
                SettingsManager.Instance.PlayerSettings.Boost = Boost.None;
        });
        m_powerUpToggle1.isOn = currentBooster == 1;
        m_powerUpToggle1.interactable = boosters > 0;
        m_powerUpToggle1.onValueChanged.AddListener(_ =>
        {
            if (AudioManager.Instance == null) return;
            AudioManager.Instance.PlayUiClip();
        });

        m_powerUpToggle2.onValueChanged.AddListener(value =>
        {
            if (value)
            {
                SettingsManager.Instance.PlayerSettings.Boost = Boost.Stun;
                PlayerPrefs.SetInt("Selected-Booster", 2);
            }
            else
                SettingsManager.Instance.PlayerSettings.Boost = Boost.None;
        });
        m_powerUpToggle2.isOn = currentBooster == 2;
        m_powerUpToggle2.interactable = boosters > 1;
        m_powerUpToggle2.onValueChanged.AddListener(_ =>
        {
            if (AudioManager.Instance == null) return;
            AudioManager.Instance.PlayUiClip();
        });

        m_powerUpToggle3.onValueChanged.AddListener(value =>
        {
            if (value)
            {
                SettingsManager.Instance.PlayerSettings.Boost = Boost.Power;
                PlayerPrefs.SetInt("Selected-Booster", 3);
            }
            else
                SettingsManager.Instance.PlayerSettings.Boost = Boost.None;
        });
        m_powerUpToggle3.isOn = currentBooster == 3;
        m_powerUpToggle3.interactable = boosters > 2;
        m_powerUpToggle3.onValueChanged.AddListener(_ =>
        {
            if (AudioManager.Instance == null) return;
            AudioManager.Instance.PlayUiClip();
        });
    }

    public override void SetInteractables(bool state)
    {
        int boosters;
        if (PlayerPrefs.HasKey("Boosters"))
            boosters = PlayerPrefs.GetInt("Boosters");
        else
            boosters = 0;
        
        m_powerUpToggle1.interactable = state && boosters > 0;
        m_powerUpToggle2.interactable = state && boosters > 1;
        m_powerUpToggle3.interactable = state && boosters > 2;
        m_backButton.interactable = state;
    }
}
