using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ResultCell : MonoBehaviour
{
    [SerializeField]
    Text _playerNameText;
    [SerializeField]
    Text _scoreText;

    public void SetCell(RankingUser user)
    {
        _playerNameText.text = user.PlayerName;
        _scoreText.text = user.Score.ToString();
    }
}
