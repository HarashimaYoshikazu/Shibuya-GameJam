using Cysharp.Threading.Tasks;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyControllerCurve : EnemyBase
{
    [SerializeField]
    [Header("”¼Œa")]
    private float _radius = 1f;

    private const float OFFSET = 4f;

    protected override void Awake()
    {
        base.Awake();
        //OnMove();
    }

    public async override void OnMove()
    {
        var min = _twoPos.MinValue.transform.position;
        var max = _twoPos.MaxValue.transform.position;
        var end = max - min;
        var _deg = 0f;

        float _timer = 0f;
        _rb.velocity = end.normalized;

        while (true)
        {
            float t = 1.0f;
            float f = 1.0f / t;
            var x = _radius * Mathf.Cos(_timer);
            var y = _radius * Mathf.Sin(2 * Mathf.PI * f * _timer);

            _rb.velocity = end.normalized + new Vector3(x, y);
            _deg += 2 * Mathf.PI / 360f;
            _timer += Time.deltaTime / OFFSET;
            SetFlip(_rb);
            await UniTask.NextFrame();
        }
    }
}
