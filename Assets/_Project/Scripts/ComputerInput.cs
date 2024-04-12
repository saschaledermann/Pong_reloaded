using UnityEngine;

public class ComputerInput : MonoBehaviour, IPaddleInput
{
    Ball m_ball;
    Vector2 m_input = new();
    bool m_move;
    PaddleSettings m_paddleSettings;
    public PaddleSettings PaddleSettings { get => m_paddleSettings; set => m_paddleSettings = value; }
    int m_bounceCounter;
    [SerializeField] ParticleSystem m_chargeParticles;
    public bool DoBoostShot 
    { 
        get
        {
            m_bounceCounter++;
            ToggleCharge(m_bounceCounter % 5 == 3);
            return m_bounceCounter % 5 == 4;
        }
    }
            

    void Start() => m_ball = FindObjectOfType<Ball>();

    public Vector2 GetInput()
    {
        if (m_ball == null) return Vector2.zero;
        var yDistance = Mathf.Abs(m_ball.transform.position.y - transform.position.y);
        var xDistance = Mathf.Abs(m_ball.transform.position.x - transform.position.x);

        if (xDistance > 0.5f && yDistance < (70 * PaddleSettings.Foresight)) m_move = true;
        if (xDistance < 0.1f || yDistance > (70 * PaddleSettings.Foresight)) m_move = false;

        if (m_move)
            m_input.x = m_ball.transform.position.x > transform.position.x ? 1f : -1f;
        else
            m_input = Vector2.zero;
        m_input.x *= PaddleSettings.Speed;
        return m_input;
    }

    public void ToggleCharge(bool value)
    {
        if (m_chargeParticles == null) return;
        if (PaddleSettings.Boost == Boost.None) return;
        if (value && m_chargeParticles.isPlaying) return;

        if (value)
            m_chargeParticles.Play();
        else
            m_chargeParticles.Stop();
    }

    public void Reset()
    {
        m_bounceCounter = 0;
        ToggleCharge(false);
    }
}
