using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(IPaddleInput))]
public class Paddle : MonoBehaviour
{
    [SerializeField] float m_speed = 3f;
    Rigidbody2D m_rigidbody;
    IPaddleInput m_inputController;

    void Awake()
    {
        m_rigidbody = GetComponent<Rigidbody2D>();
        m_inputController = GetComponent<IPaddleInput>();
    }

    void Start()
    {
        if (GameManager.Instance != null)
        {
            GameManager.Instance.restartGame += () => transform.position = new Vector2(0, transform.position.y);
        }
    }

    void FixedUpdate()
    {
        m_rigidbody.velocity = m_inputController.GetInput() * m_speed;
    }
}
