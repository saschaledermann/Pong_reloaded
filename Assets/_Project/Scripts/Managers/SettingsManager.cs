using UnityEngine;
using UnityEngine.SceneManagement;

public class SettingsManager : PersistentMonoSingleton<SettingsManager>
{
    public PaddleSettings opponentSettings;
    [SerializeField] PaddleSettings m_playerSettings;
    public PaddleSettings PlayerSettings { get => m_playerSettings; }

    protected override void Awake()
    {
        base.Awake();

        SceneManager.activeSceneChanged += (_, scene) => 
        {
            if (scene.name == "Level" && opponentSettings != null && GameManager.Instance != null)
                GameManager.Instance.SetPaddleSettings(opponentSettings, m_playerSettings);
        };
    }
}
