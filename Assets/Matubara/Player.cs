using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEditor.Searcher.SearcherWindow.Alignment;

[RequireComponent(typeof(Rigidbody2D))]

public class Player : MonoBehaviour
{
    [SerializeField, Header("プレイヤーの最大速度")] float _maxSpeed;
    [SerializeField, Header("プレイヤーが落とし物を持てる最大数")] int _inventorySize;
    Rigidbody2D _rb;
    float _h;
    float _v;
    List<GameObject> _inventory;
    float _moveSpeed;
    GameManager _gamemanager;
    bool _isStun;
    PedestalController _pedController;
    [SerializeField] Sprite _sprite;
    SpriteRenderer _spriteRenderer;
    Animator _animator;
    
    void Start()
    {
        _rb = GetComponent<Rigidbody2D>();
        _inventory = new List<GameObject>();
        _gamemanager = FindObjectOfType<GameManager>();
        _moveSpeed = _maxSpeed;
        _pedController = FindObjectOfType<PedestalController>();
        _spriteRenderer = GetComponent<SpriteRenderer>();
        _animator = GetComponent<Animator>();
    }
    void Update()
    {
        if (_isStun)
        {
            return;
        }
        _h = Input.GetAxisRaw("Horizontal");
        _v = Input.GetAxisRaw("Vertical");
        _animator.SetFloat("Horizontal", Mathf.Abs(_h));

        if (_h > 0)
        {
            this.transform.localScale = new Vector3(-1 * Mathf.Abs(this.transform.localScale.x), this.transform.localScale.y, this.transform.localScale.z);
        }
        else if (_h < 0)
        {
            this.transform.localScale = new Vector3(Mathf.Abs(this.transform.localScale.x), this.transform.localScale.y, this.transform.localScale.z);
        }
    }
    private void FixedUpdate()
    {
        if (_isStun)
        {
            return;
        }
        Vector2 dir = new Vector2(_h, _v).normalized;
        _rb.velocity = dir * _moveSpeed;
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Item") && _inventory.Count < _inventorySize)
        {
            var score = collision.gameObject.GetComponent<LostItemController>().Score;
            _gamemanager.ScoreCount(score);
            _inventory.Add(collision.gameObject);
            _moveSpeed = Mathf.Clamp(_moveSpeed -= (float)_inventory.Count / (float)_inventorySize, 1f, _maxSpeed);
            Debug.Log(_moveSpeed);
        }
        else if (collision.gameObject.CompareTag("Koban"))
        {
            _inventory.RemoveRange(0, _inventory.Count);
            _moveSpeed = _maxSpeed;
        }
    }
    public void Stun(float time)
    {
        StartCoroutine(StunTimer(time));
    }
    IEnumerator StunTimer(float time)
    {
        Sprite tmp = _spriteRenderer.sprite;
        _spriteRenderer.sprite = _sprite;
        _isStun = true;
        _rb.velocity = Vector2.zero;
        yield return new WaitForSeconds(time);
        _spriteRenderer.sprite = tmp;
        _isStun = false;
    }
}
