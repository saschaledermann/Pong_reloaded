using UnityEngine;

public class VFX : MonoBehaviour
{
    [SerializeField] AudioClip m_goalClip;

    void Start()
    {
        if (GameManager.Instance != null)
        {
            GameManager.Instance.bottomGoalScored += PlayGoalClip;
            GameManager.Instance.topGoalScored += PlayGoalClip;
        }
    }

    void PlayGoalClip()
    {
        if (AudioManager.Instance != null)
            AudioManager.Instance.PlayClip(m_goalClip, 0.3f, 0.75f);
    }
}
