using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyControllerBetweenTwoPositions : EnemyBase
{
    [SerializeField]
    [Header("2つのポジション")]
    Value<Vector3> _twoPos;

    protected override void Awake()
    {
        base.Awake();
    }

    protected override void OnMove()
    {
        throw new System.NotImplementedException();
    }
}
