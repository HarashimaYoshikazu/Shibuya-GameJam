using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;

// 日本語対応
public class GameManager : MonoBehaviour
{
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
    [SerializeField]
    private PedestalController _pedestal = null;

    private void Update()
    {
        // 制限時間のカウントダウンをする
        _timeLimit = Mathf.Clamp(_timeLimit - Time.deltaTime, 0f, _timeLimit);
        _timeText.text = _timeLimit.ToString();

        if (_timeLimit == 0f)
        {
            if (_pedestal.IsOnThePedestal)
            {
                GameClear();
            }
            else
            {
                GameOver();
            }
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
    }

    private void GameOver()
    {
        _gameOver?.Invoke();
    }
}
