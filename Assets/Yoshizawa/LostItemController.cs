using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// 日本語対応
public class LostItemController : MonoBehaviour
{
    [SerializeField]
    private int _score = 0;
    public int Score { get => _score; private set => _score = value; }

    private Collider2D _collider2D = null;
    private SpriteRenderer _spriteRenderer = null;

    private void Start()
    {
        if (TryGetComponent(out Collider2D collider2D))
        {
            _collider2D = collider2D;
            _collider2D.isTrigger = true;
        }
        if (TryGetComponent(out SpriteRenderer spriteRenderer)) _spriteRenderer = spriteRenderer;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (_collider2D == null || _spriteRenderer == null) return;

        if (collision.gameObject.tag == "Player")
        {
            _collider2D.enabled = false;
            _spriteRenderer.sortingOrder = 1000;
        }
    }
}
