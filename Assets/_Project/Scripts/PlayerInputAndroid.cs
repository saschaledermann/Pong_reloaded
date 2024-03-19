using System.Collections;
using UnityEngine;

public class PlayerInputAndroid : MonoBehaviour, IPaddleInput
{
    Vector2 m_input = new();
    Camera m_mainCamera;
    PaddleSettings m_paddleSettings;
    public PaddleSettings PaddleSettings { get => m_paddleSettings; set => m_paddleSettings = value; }
    bool m_doBoostShot;
    public bool DoBoostShot { get => m_doBoostShot; }
    float m_charge = 0;
    [SerializeField] ParticleSystem m_chargeParticles;

    void Start() => m_mainCamera = Camera.main;

    void Update()
    {
        m_input = Vector2.zero;

        if (Input.touchCount > 0)
        {
            if (m_mainCamera != null)
            {
                var touch = Input.GetTouch(0);
                var worldPos = m_mainCamera.ScreenToWorldPoint(touch.position);
                if (Mathf.Abs(worldPos.x - transform.position.x) > 1f)
                    m_input.x = (worldPos.x < transform.position.x ? -1f : 1f) * PaddleSettings.Speed;

                if (touch.phase == TouchPhase.Stationary)
                    m_charge += Time.deltaTime;

                if (m_charge > 1f)
                    ToggleCharge(true);

                if (touch.phase == TouchPhase.Ended &&
                    m_charge > 1f)
                    StartCoroutine(PowerShotRoutine());
            }
        }
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

    IEnumerator PowerShotRoutine()
    {
        m_doBoostShot = true;
        m_charge = 0f;
        ToggleCharge(false);
        yield return new WaitForSeconds(0.5f);
        m_doBoostShot = false;
    }

    public Vector2 GetInput() => m_input;
}
