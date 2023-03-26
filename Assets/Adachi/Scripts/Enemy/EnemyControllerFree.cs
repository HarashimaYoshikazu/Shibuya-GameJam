using Cysharp.Threading.Tasks;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

public class EnemyControllerFree : EnemyBase
{
    #region Inspector Variables

    [SerializeField]
    [Header("ˆÚ“®ŽžŠÔ")]
    private Value<float> _moveTime = new();

    [SerializeField]
    [Header("’âŽ~ŽžŠÔ")]
    private Value<float> _stopTime = new();

    #endregion

    #region Unity Methods

    protected override void Awake()
    {
        base.Awake();
    }

    #endregion

    #region Public Methods

    public async override void OnMove()
    {
        while (true)
        {
            var moveTime = Calculator.RandomTime(_moveTime.MinValue, _moveTime.MaxValue);
            var stopTime = Calculator.RandomTime(_stopTime.MinValue, _stopTime.MaxValue);
            var velocity = new Vector3(Calculator.RandomValue(), Calculator.RandomValue(), 0f);
            _rb.velocity = velocity * _speed;
            SetFlip(_rb);
            await UniTask.Delay(TimeSpan.FromSeconds(moveTime));
            _rb.velocity = Vector2.zero;
            await UniTask.Delay(TimeSpan.FromSeconds(stopTime));
        }
    }

    #endregion
}
