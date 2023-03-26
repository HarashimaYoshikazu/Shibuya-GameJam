using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[RequireComponent(typeof(Rigidbody2D))]

public class Player : MonoBehaviour
{
    [SerializeField] float _moveSpeed;
    [SerializeField] int _inventorySize;
    [SerializeField] string _itemTag;
    [SerializeField] float _reduce;
    [SerializeField] string _kobanTag;
    Rigidbody2D _rb;
    float _h;
    float _v;
    [SerializeField] List<GameObject> _inventory;
    // GameManager _gamemanager;
    void Start()
    {
        _rb = GetComponent<Rigidbody2D>();
        _inventory = new List<GameObject>();
        
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
        Debug.Log(_moveSpeed);
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag(_itemTag) && _inventory.Count < _inventorySize)
        {
            _inventory.Add(collision.gameObject);
            // _moveSpeed -= _inventory.Count * _reduce;
            //_moveSpeed *= 1f - (float)(_inventory.Count / _inventorySize);
        }
        else if (collision.gameObject.CompareTag(_kobanTag))
        {
            _inventory.RemoveRange(0, _inventory.Count);
        }
    }
}
