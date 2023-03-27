using Cysharp.Threading.Tasks;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Collider2D))]
public class EnemyTrash : MonoBehaviour
{
    private float _waitTime = 5f;

    private void Awake()
    {
        GetComponent<Collider2D>().isTrigger = true;
    }

    private async void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.TryGetComponent(out EnemyBase enemy))
        {
            if (enemy.TwoPos.MaxValue.position == transform.position)
            {
                await UniTask.Delay(TimeSpan.FromSeconds(_waitTime));
                Destroy(collision.gameObject);
            }
        }
    }
}
