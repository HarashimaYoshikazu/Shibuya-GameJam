using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(BoxCollider2D))]

// 日本語対応
public class PedestalController : MonoBehaviour
{
    [SerializeField]
    private float _setUpPosition = 0f;
    public bool IsOnThePedestal { get; private set; } = false;
    private GameObject _player = null;
    private Rigidbody2D _playerRigidbody2D = null;
    private Vector2 _setPos = Vector2.zero;
    private Collider2D _collder2D = null;

    private void Start()
    {
        _player = GameObject.FindGameObjectWithTag("Player");
        if (_player.TryGetComponent(out Rigidbody2D rb2D))
        {
            _playerRigidbody2D = rb2D;
        }
        _setPos = transform.position;
        _setPos.y += _setUpPosition;

        _collder2D = GetComponent<Collider2D>();
        _collder2D.isTrigger = true;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == _player.gameObject.tag)
        {
            _player.transform.position = _setPos;

            if (_playerRigidbody2D != null)
            {
                _playerRigidbody2D.simulated = false;
            }

            IsOnThePedestal = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag == _player.gameObject.tag)
        {
            IsOnThePedestal = false;
        }
    }
}
