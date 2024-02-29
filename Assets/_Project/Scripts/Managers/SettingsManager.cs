using UnityEngine.SceneManagement;

public class SettingsManager : PersistentMonoSingleton<SettingsManager>
{
    public PaddleSettings opponentSettings;

    protected override void Awake()
    {
        base.Awake();

        SceneManager.activeSceneChanged += (_, scene) => 
        {
            if (scene.name == "Level" && opponentSettings != null && GameManager.Instance != null)
                GameManager.Instance.SetOpponentSettings(opponentSettings);
        };
    }
}
