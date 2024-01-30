using System.Linq;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class Ball : MonoBehaviour
{
    [SerializeField] float m_movementSpeed;
    [SerializeField] float m_speedIncrement;
    [SerializeField] float m_maxSpeedIncrement;
    int m_numberOfPaddleBounces = 0;
    Rigidbody2D m_rigidbody;

    void Awake() => m_rigidbody = GetComponent<Rigidbody2D>();

    void Start()
    {
        MoveBall(new Vector2(Random.Range(0.5f, -0.5f), 1));
    }

    void MoveBall(Vector2 dir)
    {
        dir = dir.normalized;

        var speed = m_movementSpeed + Mathf.Clamp(m_numberOfPaddleBounces * m_speedIncrement, 0f, m_maxSpeedIncrement);
        m_rigidbody.velocity = dir * speed;
    }

    void PaddleCollision2D(Collision2D col)
    {
        var pos = transform.position;
        var paddlePos = col.transform.position;
        var paddleWidth = col.collider.bounds.size.x;
        var x = (pos.x - paddlePos.x) / paddleWidth;
        var y = paddlePos.y > 0 ? -1 : 1;
        m_numberOfPaddleBounces++;
        MoveBall(new Vector2(x, y));
    }

    void OnCollisionEnter2D(Collision2D col)
    {
        if (!col.collider.CompareTag("Paddle")) return;
        PaddleCollision2D(col);
    }
}
