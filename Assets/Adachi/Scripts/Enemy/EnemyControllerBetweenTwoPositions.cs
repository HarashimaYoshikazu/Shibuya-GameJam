using Cysharp.Threading.Tasks;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyControllerBetweenTwoPositions : EnemyBase
{
    [SerializeField]
    [Header("��~����")]
    private float _stopTime = 3f;

    protected override void Awake()
    {
        base.Awake();
    }

    #region Public Methods

    public async override void OnMove()
    {
        var pos = _twoPos.MaxValue.position;
        _rb.velocity = (pos - transform.position).normalized * _speed;

        var pattern0 = transform.position.x > pos.x && transform.position.y > pos.y;
        var pattern1 = transform.position.x > pos.x && transform.position.y < pos.y;
        var pattern2 = transform.position.x < pos.x && transform.position.y > pos.y;
        var pattern3 = transform.position.x < pos.x && transform.position.y < pos.y;
        if (pattern0)
            await UniTask.WaitUntil(() => transform.position.x < pos.x && transform.position.y < pos.y);
        else if(pattern1)
            await UniTask.WaitUntil(() => transform.position.x < pos.x && transform.position.y > pos.y);
        else if (pattern2)
            await UniTask.WaitUntil(() => transform.position.x > pos.x && transform.position.y < pos.y);
        else if (pattern3)
            await UniTask.WaitUntil(() => transform.position.x > pos.x && transform.position.y > pos.y);


        _rb.velocity = Vector2.zero;
        transform.position = pos;

        Destroy(gameObject);
    }

    #endregion
}
