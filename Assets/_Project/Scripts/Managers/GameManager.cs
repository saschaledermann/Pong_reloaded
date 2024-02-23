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

    Ball m_ball;

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
                topGoalScored?.Invoke();
            };

            m_ball.bottomGoalScored += () =>
            {
                Stopped = true;
                bottomGoalScored?.Invoke();
            };
        }
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space) && m_ball != null)
        {
            StartBall();
        }
    }

    async void StartBall()
    {
        if (m_ball == null) return;

        m_ball.Reset();

        await Task.Delay(1000);

        m_ball.MoveBall(new Vector2(UnityEngine.Random.Range(0.5f, -0.5f), 1));
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
}
