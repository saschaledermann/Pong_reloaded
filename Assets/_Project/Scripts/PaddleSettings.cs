using UnityEngine;

[CreateAssetMenu]
public class PaddleSettings : ScriptableObject
{
    [SerializeField] [Range(1f, 10f)] float m_speed;
    public float Speed { get => m_speed; }
    [SerializeField] [Range(0f, 1f)] float m_foresight;
    public float Foresight { get => m_foresight; }
}
