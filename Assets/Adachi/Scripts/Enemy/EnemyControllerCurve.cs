using Cysharp.Threading.Tasks;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyControllerCurve : EnemyBase
{
    [SerializeField]
    [Header("”¼Œa")]
    private float _radius = 1f;

    private const float OFFSET = 1f;

    protected override void Awake()
    {
        base.Awake();
        OnMove();
    }

    public async override void OnMove()
    {
        var min = _twoPos.MinValue.transform.position;
        var max = _twoPos.MaxValue.transform.position;
        var end = max - min;
        var _deg = 0f;

        var pos = _twoPos.MaxValue.position;
        var pattern0 = transform.position.x > pos.x && transform.position.y > pos.y;
        var pattern1 = transform.position.x > pos.x && transform.position.y < pos.y;
        var pattern2 = transform.position.x < pos.x && transform.position.y > pos.y;
        var pattern3 = transform.position.x < pos.x && transform.position.y < pos.y;
        if (pattern0)
        {
            while (true)
            {
                float time = 1.0f;
                float f = 1.0f / time;
                var x = _radius * Mathf.Cos(Time.time);
                var y = _radius * Mathf.Sin(2 * Mathf.PI * f * Time.time);
                _rb.velocity = new Vector2(x, y) * end.magnitude;
                _deg += 2 * Mathf.PI / 360f;
                if (transform.position.x < pos.x && transform.position.y < pos.y) break;
                await UniTask.NextFrame();
            }
            pos = new Vector3(_twoPos.MaxValue.position.x, _twoPos.MaxValue.position.y, _twoPos.MaxValue.position.z);
        }
        else if (pattern1)
        {
            while (true)
            {
                float time = 1.0f;
                float f = 1.0f / time;
                var x = _radius * Mathf.Cos(Time.time);
                var y = _radius * Mathf.Sin(2 * Mathf.PI * f * Time.time);
                _rb.velocity = new Vector2(x, y) * end.magnitude;
                _deg += 2 * Mathf.PI / 180f;
                if (transform.position.x < pos.x && transform.position.y > pos.y) break;
                await UniTask.NextFrame();
            }
            pos = new Vector3(_twoPos.MaxValue.position.x, _twoPos.MaxValue.position.y, _twoPos.MaxValue.position.z);
        }
        else if (pattern2)
        {
            while (true)
            {
                float time = 1.0f;
                float f = 1.0f / time;
                var x = _radius * Mathf.Cos(Time.time);
                var y = _radius * Mathf.Sin(2 * Mathf.PI * f * Time.time);
                _rb.velocity = new Vector2(x, y) * end.magnitude;
                _deg += 2 * Mathf.PI / 180f;
                if (transform.position.x > pos.x && transform.position.y < pos.y) break;
                await UniTask.NextFrame();
            }
            pos = new Vector3(_twoPos.MaxValue.position.x, _twoPos.MaxValue.position.y, _twoPos.MaxValue.position.z);
        }
        else if (pattern3)
        {
            while (true)
            {
                float time = 1.0f;
                float f = 1.0f / time;
                var x = _radius * Mathf.Cos(Time.time);
                var y = _radius * Mathf.Sin(2 * Mathf.PI * f * Time.time);
                _rb.velocity = new Vector2(x, y) * end.magnitude;
                _deg += 2 * Mathf.PI / 180f;
                if (transform.position.x > pos.x && transform.position.y > pos.y) break;
                await UniTask.NextFrame();
            }
            pos = new Vector3(_twoPos.MaxValue.position.x, _twoPos.MaxValue.position.y, _twoPos.MaxValue.position.z);
        }

        Destroy(gameObject);
    }
}
