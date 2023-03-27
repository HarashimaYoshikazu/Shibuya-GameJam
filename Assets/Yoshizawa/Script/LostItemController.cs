using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// 日本語対応
public class LostItemController : MonoBehaviour
{
    [SerializeField]
    private int _score = 0;
    public int Score { get => _score; private set => _score = value; }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            Destroy(gameObject);
        }
    }
}
