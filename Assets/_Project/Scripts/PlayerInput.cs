using UnityEngine;

public class PlayerInput : MonoBehaviour, IPaddleInput
{
    Vector2 m_input = new();

    void Update()
    {
        m_input.x = Input.GetAxisRaw("Horizontal");
    }

    public Vector2 GetInput() => m_input;
}
