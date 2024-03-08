using System;
using System.Threading.Tasks;
using UnityEngine;

public class GameManager : MonoSingleton<GameManager>
{
    public Action topGoalScored;
    public Action bottomGoalScored;
    public Action restartGame;
    public Action<bool> gameOver;

    public bool Paused { get; set; }
    public bool Stopped { get; set; }

    [SerializeField] Paddle m_opponent;
    [SerializeField] Paddle m_player;

    Ball m_ball;
    bool m_playerScored;

    protected override void Awake()
    {
        base.Awake();
        Paused = false;
        Stopped = false;
    }

    void Start()
    {
        m_ball = FindObjectOfType<Ball>();
        if (m_ball != null)
        {
            m_ball.topGoalScored += () =>
            {
                Stopped = true;
                m_playerScored = false;
                topGoalScored?.Invoke();
                StartBall(1);
            };

            m_ball.bottomGoalScored += () =>
            {
                Stopped = true;
                m_playerScored = true;
                bottomGoalScored?.Invoke();
                StartBall(1);
            };
        }

        m_playerScored = UnityEngine.Random.Range(0, 2) == 1;
        StartBall();
    }

    async void StartBall(int delay = 1)
    {
        if (m_ball == null) return;

        m_ball.Reset();

        delay *= 1000;
        await Task.Delay(delay);

        m_ball.MoveBall(new Vector2(UnityEngine.Random.Range(0.5f, -0.5f), m_playerScored ? -1 : 1));
        Stopped = false;
    }

    public void TogglePause()
    {
        Paused = !Paused;
        Time.timeScale = Paused ? 0f : 1f;
    }

    public void RestartGame()
    {
        restartGame.Invoke();
        StartBall();
    }

    public void SetPaddleSettings(PaddleSettings opponentSettings, PaddleSettings playerSettings)
    {
        if (m_opponent != null)
            m_opponent.PaddleSettings = opponentSettings;
        if (m_player != null)
            m_player.PaddleSettings = playerSettings;
    }
}
