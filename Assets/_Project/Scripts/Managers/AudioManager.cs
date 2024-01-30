using UnityEngine;

public class AudioManager : MonoSingleton<AudioManager>
{
    GameObject m_backgroundMusicGO;

    public void PlayClip(Vector3 position, AudioClip clip, float pitch = 1f, bool variablePitch = false)
    {
        var clipGO = new GameObject("AudioClip");
        clipGO.transform.position = position;
        clipGO.transform.parent = transform;

        var source = clipGO.AddComponent<AudioSource>();
        source.loop = false;
        source.clip = clip;
        source.pitch = variablePitch ? pitch + Random.Range(-0.1f, 0.1f) : pitch;
        source.Play();

        Destroy(clipGO, clip.length + 0.25f);
    }
}
