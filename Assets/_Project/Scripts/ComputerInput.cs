using UnityEngine;

public class ComputerInput : MonoBehaviour, IPaddleInput
{
    Ball m_ball;
    Vector2 m_input = new();

    void Start()
    {
        m_ball = FindObjectOfType<Ball>();
    }

    public Vector2 GetInput()
    {
        if (m_ball == null) return Vector2.zero;
        if (Mathf.Abs(m_ball.transform.position.x - transform.position.x) < 0.5f) return Vector2.zero;
        m_input.x = m_ball.transform.position.x > transform.position.x ? 1f : -1f;
        return m_input;
    }
}
