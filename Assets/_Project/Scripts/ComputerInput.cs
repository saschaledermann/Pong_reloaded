using UnityEngine;

public class ComputerInput : MonoBehaviour, IPaddleInput
{
    [Range(0f, 1f)] public float foresight = 0.5f;
    Ball m_ball;
    Vector2 m_input = new();
    bool m_move;

    void Start()
    {
        m_ball = FindObjectOfType<Ball>();
    }

    public Vector2 GetInput()
    {
        if (m_ball == null) return Vector2.zero;
        var yDistance = Mathf.Abs(m_ball.transform.position.y - transform.position.y);
        var xDistance = Mathf.Abs(m_ball.transform.position.x - transform.position.x);

        if (xDistance > 0.5f && yDistance < (7 * foresight)) m_move = true;
        if (xDistance < 0.1f || yDistance > (7 * foresight)) m_move = false;

        if (m_move)
            m_input.x = m_ball.transform.position.x > transform.position.x ? 1f : -1f;
        else
            m_input = Vector2.zero;
        return m_input;
    }
}
