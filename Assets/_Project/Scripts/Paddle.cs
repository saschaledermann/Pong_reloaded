using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(IPaddleInput))]
public class Paddle : MonoBehaviour
{
    [SerializeField] PaddleSettings m_paddleSettings;
    public PaddleSettings PaddleSettings
    {
        get
        {
            if (m_paddleSettings == null)
                m_paddleSettings = ScriptableObject.CreateInstance<PaddleSettings>();
            return m_paddleSettings;
        }
        set
        {
            m_paddleSettings = value;
            m_inputController.PaddleSettings = value;
        }
    }
    Rigidbody2D m_rigidbody;
    IPaddleInput m_inputController;
    public bool HasEffect { get; set; }
    float m_effectDuration = 1.5f;
    float m_effectTime = 0f;

    void Awake()
    {
        m_rigidbody = GetComponent<Rigidbody2D>();
        m_inputController = GetComponent<IPaddleInput>();
    }

    void Start()
    {
        if (m_paddleSettings != null && m_inputController != null)
            m_inputController.PaddleSettings = PaddleSettings;
        
        if (GameManager.Instance != null)
            GameManager.Instance.restartGame += () => transform.position = new Vector2(0, transform.position.y);
    }

    void FixedUpdate()
    {
        var velocity = m_inputController.GetInput();
        if (HasEffect)
        {
            velocity *= 0.5f;
            m_effectTime += Time.fixedDeltaTime;
            if (m_effectTime >= m_effectDuration)
            {
                HasEffect = false;
                m_effectTime = 0;
            }
        }
        m_rigidbody.velocity = velocity;
    }

    public bool IsBoostShot(out Boost boost)
    {
        boost = PaddleSettings.Boost;
        return m_inputController.DoBoostShot;
    }
}
