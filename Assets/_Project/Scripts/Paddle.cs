using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class Paddle : MonoBehaviour
{
    [SerializeField] float m_speed = 3f;
    Rigidbody2D m_rigidbody;

    void Awake() => m_rigidbody = GetComponent<Rigidbody2D>();

    void FixedUpdate()
    {
        m_rigidbody.velocity = new Vector2(Input.GetAxisRaw("Horizontal") * m_speed, 0);
    }
}
