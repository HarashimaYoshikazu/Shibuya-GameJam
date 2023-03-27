using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MiniMap : MonoBehaviour
{
    [SerializeField]
    Player _player;

    [SerializeField]
    Image _playerIcon;

    private void Update()
    {
        _playerIcon.rectTransform.anchoredPosition = _player.transform.position * 13f;
    }
}
