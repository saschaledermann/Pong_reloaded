using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public Action<bool> gameOver;

    [Header("Score objects")]
    [SerializeField] TMP_Text m_topScoreText;
    [SerializeField] TMP_Text m_bottomScoreText;
    [SerializeField] Toggle m_pauseToggle;
    [SerializeField] PauseView m_pauseView;
    [SerializeField] Toggle m_audioToggle;
    int m_topScore;
    int m_bottomScore;

    void Start()
    {
        m_topScoreText.text = m_bottomScoreText.text = "0";
        if (GameManager.Instance != null)
        {
            GameManager.Instance.topGoalScored += TopGoalScored;
            GameManager.Instance.bottomGoalScored += BottomGoalScored;
            GameManager.Instance.restartGame += ResetScore;
            gameOver += GameManager.Instance.gameOver;

            m_pauseToggle.onValueChanged.AddListener(value =>
            {
                GameManager.Instance.TogglePause(value);
                m_pauseView.TogglePause();
                m_pauseToggle.interactable = !value;
            });
        }

        if (AudioManager.Instance != null)
            m_audioToggle.isOn = AudioManager.Instance.Paused;
        else
            m_audioToggle.interactable = false;
        
        m_audioToggle.onValueChanged.AddListener(value => 
        {
            if (AudioManager.Instance != null)
                AudioManager.Instance.ToggleAudio(value);
        });

        m_pauseToggle.onValueChanged.AddListener(_ =>
        {
            if (AudioManager.Instance == null) return;
            AudioManager.Instance.PlayUiClip();
        });
        m_audioToggle.onValueChanged.AddListener(_ =>
        {
            if (AudioManager.Instance == null) return;
            AudioManager.Instance.PlayUiClip();
        });
    }

    void BottomGoalScored()
    {
        m_topScore++;
        m_topScoreText.text = m_topScore.ToString();
        
        if (m_topScore >= 5)
            gameOver?.Invoke(false);
    }

    void TopGoalScored()
    {
        m_bottomScore++;
        m_bottomScoreText.text = m_bottomScore.ToString();
        
        if (m_bottomScore >= 5)
            gameOver?.Invoke(true);
    }

    void ResetScore()
    {
        m_topScore = m_bottomScore = 0;
        m_topScoreText.text = m_bottomScoreText.text = 0.ToString();
    }
}
