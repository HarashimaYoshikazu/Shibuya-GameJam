using Cysharp.Threading.Tasks;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyControllerCurve : EnemyBase
{
    [SerializeField]
    [Header("�ړ�����")]
    private float _time = 5f;

    [SerializeField]
    [Header("���a")]
    private float _radius = 0.5f;

    [SerializeField]
    private float _wave = 5f;

    private float _deg = 0f;

    private const float OFFSET = 5f;

    protected override void Awake()
    {
        base.Awake();
        OnMove();
    }

    public async override void OnMove()
    {
        var min = _twoPos.MinValue.transform.position;
        var max = _twoPos.MaxValue.transform.position;
        var center = (min + max) * 0.5f;//2�_�̊�
        var firstCenter = (min + center) * 0.5f;//�ŏ��̓_�ƂQ�_�̊ԂƂ̊�
        var endCenter = (center + max) * 0.5f;//�Q�_�̊ԂƍŌ�̓_�Ƃ̊�
        //_rb.velocity = (transform.position - min).normalized * _speed;

        while (true)
        {
            var x = _radius * Mathf.Cos(_deg);
            var y = _radius * Mathf.Sin(_deg);
            var z = 0f;
            _rb.velocity = new Vector3(x, y, z);
            _deg += 2 * Mathf.PI / 360f;
            //_rb.velocity = new Vector3(Mathf.Cos(_speed), _rb.velocity.y);
            await UniTask.NextFrame();
        }

        Destroy(gameObject);
    }
}
