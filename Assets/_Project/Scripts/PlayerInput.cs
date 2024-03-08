using UnityEngine;

public class PlayerInput : MonoBehaviour, IPaddleInput
{
    Vector2 m_input = new();
    PaddleSettings m_paddleSettings;
    public PaddleSettings PaddleSettings { get => m_paddleSettings; set => m_paddleSettings = value; }
    bool m_doBoostShot;
    public bool DoBoostShot { get => m_doBoostShot; }
    float m_charge = 0;

    void Update()
    {
        m_input.x = PaddleSettings.Speed * Input.GetAxisRaw("Horizontal");
        if (Input.GetMouseButton(0))
            m_charge += Time.deltaTime;
        else
            m_charge = 0;

        m_doBoostShot = m_charge > 1f;
    }

    public Vector2 GetInput() => m_input;
}
