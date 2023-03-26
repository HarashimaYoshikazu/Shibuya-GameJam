using Cysharp.Threading.Tasks;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(Collider2D))]
public abstract class EnemyBase : MonoBehaviour
{
    #region Properties

    public Value<Transform> TwoPos => _twoPos;

    #endregion

    #region Inspector Variables

    [SerializeField]
    [Header("プレイヤーのスタン時間")]
    private float _stunTime = 2f;

    [SerializeField]
    [Header("スピード")]
    protected float _speed = 1f;

    [SerializeField]
    [Header("2つのポジション")]
    protected Value<Transform> _twoPos;

    #endregion

    #region Member Variables

    protected Rigidbody2D _rb = null;
    protected Vector3 _velocity;
    protected bool _isPausing = false;
    private float _angle = 45f;

    #endregion

    #region Unity Methods

    protected virtual void Awake()
    {
        _rb = GetComponent<Rigidbody2D>();
    }

    #endregion

    #region Public Methods

    public void Init(Transform startPos, Transform endPos)
    {
        _twoPos.SetValue(startPos, endPos);
    }

    public virtual void Pause()
    {
        _isPausing = true;
        _velocity = _rb.velocity;
        _rb.velocity = Vector2.zero;
        _rb.constraints = RigidbodyConstraints2D.FreezePosition;
    }

    public virtual void Resume()
    {
        _isPausing = false;
        _rb.constraints = RigidbodyConstraints2D.None;
        _rb.constraints = RigidbodyConstraints2D.FreezeRotation;
        _rb.velocity = _velocity;
    }

    #endregion

    #region Private Methods

    public abstract void OnMove();

    private void OnTriggerStay2D(Collider2D collision)
    {
        Vector3 posDelta = collision.transform.position - transform.position;
        float targetAngle = Vector3.Angle(_rb.velocity, posDelta);
        if (targetAngle < _angle)
        {
            Debug.Log("");
            //if(TryGetComponent<Player>)
            //{

            //}
        }
    }

    #endregion
}
