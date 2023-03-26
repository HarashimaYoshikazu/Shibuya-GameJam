using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[RequireComponent(typeof(Rigidbody2D))]

public class Player : MonoBehaviour
{
    [SerializeField] float _moveSpeed;
    Rigidbody2D _rb;
    float _h;
    float _v;
    void Start()
    {
        _rb = GetComponent<Rigidbody2D>();

    }

    void Update()
    {
        _h = Input.GetAxisRaw("Horizontal");
        _v = Input.GetAxisRaw("Vertical");
    }
    private void FixedUpdate()
    {
        Vector2 dir = new Vector2(_h, _v).normalized;
        _rb.velocity = dir * _moveSpeed;
    }
}
