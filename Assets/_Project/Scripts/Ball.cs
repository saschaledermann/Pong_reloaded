using System;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(TrailRenderer))]
public class Ball : MonoBehaviour
{
    public Action topGoalScored;
    public Action bottomGoalScored;

    [SerializeField] float m_movementSpeed;
    [SerializeField] float m_speedIncrement;
    [SerializeField] float m_maxSpeedIncrement;

    int m_numberOfPaddleBounces = 0;
    Rigidbody2D m_rigidbody;
    TrailRenderer m_trailrenderer;

    void Awake()
    {
        m_rigidbody = GetComponent<Rigidbody2D>();
        m_trailrenderer = GetComponent<TrailRenderer>();
    }

    void Start()
    {
        MoveBall(new Vector2(UnityEngine.Random.Range(0.5f, -0.5f), 1));
    }

    public void MoveBall(Vector2 dir)
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

    void Reset()
    {
        m_rigidbody.velocity = Vector2.zero;
        transform.position = Vector2.zero;
        m_numberOfPaddleBounces = 0;
        m_trailrenderer.Clear();
    }

    void OnCollisionEnter2D(Collision2D col)
    {
        if (!col.collider.CompareTag("Paddle")) return;
        PaddleCollision2D(col);
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.CompareTag("Top Goal"))
        {
            topGoalScored?.Invoke();
            Reset();
        }
        else if (col.CompareTag("Bottom Goal"))
        {
            bottomGoalScored?.Invoke();
            Reset();
        }
    }
}
