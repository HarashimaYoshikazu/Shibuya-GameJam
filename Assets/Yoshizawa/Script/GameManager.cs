using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;

// 日本語対応
public class GameManager : MonoBehaviour
{
    private static GameManager _instance;
    public static GameManager Instance;
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

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(this.gameObject);
            return;
        }
        Instance= this;
        IsPause= true;
    }

    private void Start()
    {
        _pedestal = FindObjectOfType<PedestalController>();
        _scoreText.text = _score.ToString();
    }

    private void Update()
    {
        if(IsPause)
        {
            return;
        }
        // 制限時間のカウントダウンをする
        _timeLimit = Mathf.Clamp(_timeLimit - Time.deltaTime, 0f, _timeLimit);
        _timeText.text = _timeLimit.ToString("F2");

        if (_timeLimit == 0f && !_isGameFinish)
        {
            _resultPanel.SetupResultPanel("ああ",100);
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
        _scoreText.text = _score.ToString();
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
