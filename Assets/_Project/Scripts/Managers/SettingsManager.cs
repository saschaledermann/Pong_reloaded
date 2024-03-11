using UnityEngine;
using UnityEngine.SceneManagement;

public class SettingsManager : PersistentMonoSingleton<SettingsManager>
{
    [SerializeField] PaddleSettings[] m_opponentSettings;
    public PaddleSettings OpponentSettings { get => m_opponentSettings[Level - 1]; }
    [SerializeField] PaddleSettings m_playerSettings;
    public PaddleSettings PlayerSettings { get => m_playerSettings; }
    public int Level { get; set; }

    protected override void Awake()
    {
        base.Awake();

        SceneManager.activeSceneChanged += (_, scene) => 
        {
            if (scene.name == "Level" && OpponentSettings != null && GameManager.Instance != null)
                GameManager.Instance.SetPaddleSettings(OpponentSettings, m_playerSettings);
        };
    }
}
