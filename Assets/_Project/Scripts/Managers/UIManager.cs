using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    [Header("Score objects")]
    [SerializeField] TMP_Text m_topScoreText;
    [SerializeField] TMP_Text m_bottomScoreText;
    [SerializeField] Button m_pauseButton;
    int m_topScore;
    int m_bottomScore;

    void Start()
    {
        m_topScoreText.text = m_bottomScoreText.text = "0";
        if (GameManager.Instance != null)
        {
            GameManager.Instance.topGoalScored += TopGoalScored;
            GameManager.Instance.bottomGoalScored += BottomGoalScored;
            m_pauseButton.onClick.AddListener(() =>
            {
                GameManager.Instance.TogglePause();
                m_pauseButton.transform.GetChild(0).GetComponent<TMP_Text>().text = GameManager.Instance.Paused ? "|>" : "||";
            });
        }
    }

    void BottomGoalScored()
    {
        m_topScore++;
        m_topScoreText.text = m_topScore.ToString();
    }

    void TopGoalScored()
    {
        m_bottomScore++;
        m_bottomScoreText.text = m_bottomScore.ToString();
    }
}
