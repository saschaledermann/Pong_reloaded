using UnityEngine;

[CreateAssetMenu]
public class PaddleSettings : ScriptableObject
{
    [SerializeField] [Range(1f, 10f)] float m_speed;
    public float Speed { get => m_speed; }
    [SerializeField] [Range(0f, 1f)] float m_foresight;
    public float Foresight { get => m_foresight; }
    [SerializeField] Boost m_boost = Boost.None;
    public Boost Boost { get => m_boost; set => m_boost = value; }
}
