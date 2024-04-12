using System.Collections;
using UnityEngine;

public class PlayerInput : MonoBehaviour, IPaddleInput
{
    Vector2 m_input = new();
    PaddleSettings m_paddleSettings;
    public PaddleSettings PaddleSettings { get => m_paddleSettings; set => m_paddleSettings = value; }
    bool m_doBoostShot;
    public bool DoBoostShot { get => m_doBoostShot; }
    float m_charge = 0;

    void Update()
    {
        m_input.x = PaddleSettings.Speed * Input.GetAxisRaw("Horizontal");

        if (Input.GetMouseButton(0))
            m_charge += Time.deltaTime;
        else
        {
            if (m_charge > 1f && !m_doBoostShot)
                StartCoroutine(PowerShotRoutine());
            
            m_charge = 0;
        }
    }

    IEnumerator PowerShotRoutine()
    {
        m_doBoostShot = true;
        yield return new WaitForSeconds(0.35f);
        m_doBoostShot = false;
    }

    public Vector2 GetInput() => m_input;

    public void Reset()
    {
        throw new System.NotImplementedException();
    }
}
