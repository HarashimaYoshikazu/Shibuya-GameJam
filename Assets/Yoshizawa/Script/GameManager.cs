using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;

// 日本語対応
public class GameManager : MonoBehaviour
{
    private static GameManager _instance;
    public static GameManager Instance => _instance;
    [SerializeField]
    private float _timeLimit = 0f;
    [SerializeField]
    private Text _timeText = null;
    private int _score = 0;
    [SerializeField]
    private Text _scoreText = null;
    [SerializeField]
    private UnityEvent _gameClear = new();
    [SerializeField]
    private UnityEvent _gameOver = new();
    private bool _isGameFinish = false;
    [SerializeField]
    private PedestalController _pedestal = null;

    [SerializeField]
    private ResultPanel _resultPanel;

    [SerializeField]
    EnemyGenerator _enemyGenerator;

    public bool IsPause = false;

    private string _playerName;
    public void SetPlayerName(string name)
    {
        _playerName = name;
    }

    private void Awake()
    {
        if (_instance != null && _instance != this)
        {
            Destroy(this.gameObject);
            return;
        }
        _instance = this;
        IsPause = true;
    }

    private void Start()
    {
        _pedestal = FindObjectOfType<PedestalController>();
        _scoreText.text = "スコア:" + _score.ToString();
        _timeText.text = "残り時間:" + _timeLimit.ToString("F2");

    }

    private void Update()
    {
        if (IsPause)
        {
            return;
        }
        // 制限時間のカウントダウンをする
        _timeLimit = Mathf.Clamp(_timeLimit - Time.deltaTime, 0f, _timeLimit);
        _timeText.text = "残り時間:" + _timeLimit.ToString("F2");

        if (_timeLimit == 0f && !_isGameFinish)
        {
            _resultPanel.SetupResultPanel(_playerName, _score);
            _enemyGenerator.IsEnd = true;
            if (_pedestal.IsOnThePedestal)
            {
                GameClear();
            }
            else
            {
                GameOver();
            }

            _isGameFinish = true;
        }
    }

    public void ScoreCount(int score)
    {
        _score += score;
        _scoreText.text = "スコア:" + _score.ToString();
    }

    private void GameClear()
    {
        _gameClear?.Invoke();
        Debug.Log("Clear");
    }

    private void GameOver()
    {
        _gameOver?.Invoke();
        Debug.Log("Over");
    }

    public void OnStart()
    {
        _enemyGenerator.Generate();
    }
}
