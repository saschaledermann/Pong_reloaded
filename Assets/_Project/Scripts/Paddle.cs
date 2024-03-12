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
        {
            GameManager.Instance.restartGame += Reset;
            GameManager.Instance.bottomGoalScored += Reset;
            GameManager.Instance.topGoalScored += Reset;
        }
    }

    void OnDisable()
    {
        if (GameManager.Instance != null)
        {
            GameManager.Instance.restartGame -= Reset;
            GameManager.Instance.bottomGoalScored -= Reset;
            GameManager.Instance.topGoalScored -= Reset;
        }
    }

    void FixedUpdate()
    {
        var velocity = m_inputController.GetInput();
        if (HasEffect)
            velocity *= 0.33f;

        m_rigidbody.velocity = velocity;
    }

    void Reset()
    {
        HasEffect = false;
        transform.position = new Vector2(0, transform.position.y);
    }

    public bool IsBoostShot(out Boost boost)
    {
        boost = PaddleSettings.Boost;
        return m_inputController.DoBoostShot;
    }
}
