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
    [SerializeField] Button m_pauseButton;
    [SerializeField] PauseView m_pauseView;
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

            m_pauseButton.onClick.AddListener(() =>
            {
                GameManager.Instance.TogglePause();
                m_pauseButton.transform.GetChild(0).GetComponent<TMP_Text>().text = GameManager.Instance.Paused ? "|>" : "||";
                m_pauseView.TogglePause();
                m_pauseButton.interactable = false;
            });
        }
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
