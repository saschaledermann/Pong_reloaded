using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class AudioManager : PersistentMonoSingleton<AudioManager>
{
    [SerializeField] AudioClip m_mainMenuMusic;
    [SerializeField] AudioClip m_levelMusic;
    [SerializeField] AudioClip m_bossMusic;
    AudioSource m_backgroundMusicSource;
    List<AudioSource> m_audioSources = new();
    public bool Paused { get; private set; }

    protected override void Awake()
    {
        base.Awake();

        Paused = false;

        SceneManager.activeSceneChanged += (_, scene) =>
        {
            if (m_backgroundMusicSource != null)
            {
                m_backgroundMusicSource.Stop();
                m_audioSources.Remove(m_backgroundMusicSource);
                m_backgroundMusicSource = null;
                Destroy(m_backgroundMusicSource.gameObject);
            }

            if (scene.name == "Main")
                StartBackgroundMusic(m_mainMenuMusic);
            else if (scene.name == "Level" && SettingsManager.Instance.Level % 3 == 0)
                StartBackgroundMusic(m_bossMusic);
            else
                StartBackgroundMusic(m_levelMusic);
        };
    }

    public void PlayClip(Vector3 position, AudioClip clip, float pitch = 1f, bool variablePitch = false)
    {
        if (Paused) return;

        var clipGO = new GameObject("AudioClip");
        clipGO.transform.position = position;
        clipGO.transform.parent = transform;

        var source = clipGO.AddComponent<AudioSource>();
        source.loop = false;
        source.clip = clip;
        source.pitch = variablePitch ? pitch + Random.Range(-0.1f, 0.1f) : pitch;
        source.Play();
        m_audioSources.Add(source);

        Destroy(clipGO, clip.length + 0.25f);
    }

    public void PlayClip(AudioClip clip, float volume = 1f, float pitch = 1f)
    {
        if (Paused) return;

        var clipGO = new GameObject("AudioClip");
        clipGO.transform.position = Vector3.zero;
        clipGO.transform.parent = transform;

        var source = clipGO.AddComponent<AudioSource>();
        source.loop = false;
        source.clip = clip;
        source.volume = volume;
        source.pitch = pitch;
        source.Play();
        m_audioSources.Add(source);

        Destroy(clipGO, clip.length + 0.25f);
    }

    void StartBackgroundMusic(AudioClip clip)
    {
        if (m_backgroundMusicSource == null)
            SetupMusicGameObject();
        m_backgroundMusicSource.clip = clip;
        m_backgroundMusicSource.loop = true;
        m_backgroundMusicSource.volume = 0.15f;
        m_backgroundMusicSource.Play();
        m_audioSources.Add(m_backgroundMusicSource);
    }

    private void SetupMusicGameObject()
    {
        var go = new GameObject("BackgroundMusic");
        m_backgroundMusicSource = go.AddComponent<AudioSource>();
    }

    public void ToggleAudio(bool value)
    {
        Paused = value;
        PlayerPrefs.SetInt("Audio", value ? 1 : 0);

        m_audioSources.RemoveAll(source => source == null);

        foreach (var source in m_audioSources)
            source.volume = value ? 1f : 0f;

        if (m_backgroundMusicSource != null)
            m_backgroundMusicSource.volume = value ? 0.15f : 0f;
    }
}
