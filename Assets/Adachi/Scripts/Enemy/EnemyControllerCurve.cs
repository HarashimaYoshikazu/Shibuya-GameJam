using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class EnemyControllerCurve : EnemyBase
{
    [SerializeField]
    [Header("ˆÚ“®ŽžŠÔ")]
    private float _time = 5f;

    [SerializeField]
    private Vector3[] _pos;

    protected override void Awake()
    {
        base.Awake();
        OnMove();
    }

    public async override void OnMove()
    {
        //var min = _twoPos.MinValue.transform.position;
        //var max = _twoPos.MaxValue.transform.position;
        //var path = new Vector3[]{max, min, max - min};
        await transform
            .DOPath(_pos,
                _time, PathType.CubicBezier)
            .AsyncWaitForCompletion();
        //.DOMove(_twoPos.MaxValue.position, _time)
        //.SetEase(Ease.InBack)

        transform.DOKill();
        Destroy(gameObject);
    }
}
