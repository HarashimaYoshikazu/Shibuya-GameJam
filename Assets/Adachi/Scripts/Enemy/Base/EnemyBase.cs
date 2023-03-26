using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(Collider2D))]
public abstract class EnemyBase : MonoBehaviour
{
    #region Inspector Variables

    [SerializeField]
    [Header("スピード")]
    protected float _speed = 1f;

    #endregion

    #region Member Variables

    protected Rigidbody2D _rb = null;
    private Vector3 velocity;

    #endregion

    #region Unity Methods

    protected virtual void Awake()
    {
        _rb = GetComponent<Rigidbody2D>();
        OnMove();
    }

    #endregion

    #region Private Methods

    public virtual void Pause()
    {
        velocity = _rb.velocity;
        _rb.velocity = Vector2.zero;
        _rb.constraints = RigidbodyConstraints2D.FreezePosition;
    }

    public virtual void Resume()
    {
        _rb.constraints = RigidbodyConstraints2D.None;
        _rb.velocity = velocity;
    }

    protected abstract void OnMove();

    #endregion
}
