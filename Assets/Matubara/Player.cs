using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[RequireComponent(typeof(Rigidbody2D))]

public class Player : MonoBehaviour
{
    [SerializeField] float _moveSpeed;
    [SerializeField] int _inventorySize;
    [SerializeField] string _itemTag;
    Rigidbody2D _rb;
    float _h;
    float _v;
    [SerializeField] GameObject[] _inventory;
    int _index = 0;
    void Start()
    {
        _rb = GetComponent<Rigidbody2D>();
        _inventory = new GameObject[_inventorySize];
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
        Debug.Log(_rb.velocity.magnitude);
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag(_itemTag) && _inventory[_inventory.Length - 1] == null)
        {
            _inventory[_index] = collision.gameObject;
            _index++;
        }
    }
}
