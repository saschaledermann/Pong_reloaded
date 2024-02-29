using UnityEngine;

public class PlayerInput : MonoBehaviour, IPaddleInput
{
    Vector2 m_input = new();
    PaddleSettings m_paddleSettings;
    public PaddleSettings PaddleSettings { get => m_paddleSettings; set => m_paddleSettings = value; }

    void Update()
    {
        m_input.x = PaddleSettings.Speed * Input.GetAxisRaw("Horizontal");
    }

    public Vector2 GetInput() => m_input;
}
