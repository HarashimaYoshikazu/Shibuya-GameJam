using System.Collections;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]

public class Player : MonoBehaviour
{
    [SerializeField, Header("プレイヤーの最大速度")] float _maxSpeed;
    [SerializeField, Header("プレイヤーが落とし物を持てる最大数")] int _inventorySize;
    [SerializeField, Header("疲労時に表示する画像のゲームオブジェクト")] GameObject _sweat;
    Rigidbody2D _rb;
    float _h;
    float _v;
    /// <summary> 落とし物を拾った数を保持しておく変数 </summary>
    float _inventory;
    float _moveSpeed;
    GameManager _gamemanager;
    bool _isStun;
    PedestalController _pedController;
    [SerializeField, Header("スタンしたときに切り替える画像")] Sprite _stunsprite;
    SpriteRenderer _spriteRenderer;
    Animator _animator;
    int _tmpScore = 0;

    void Start()
    {
        _rb = GetComponent<Rigidbody2D>();
        _gamemanager = FindObjectOfType<GameManager>();
        _moveSpeed = _maxSpeed;
        _pedController = FindObjectOfType<PedestalController>();
        _spriteRenderer = GetComponent<SpriteRenderer>();
        _animator = GetComponent<Animator>();
    }
    void Update()
    {
        if (_isStun || _pedController.IsOnThePedestal == true || GameManager.Instance.IsPause)
        {
            return;
        }
        // 入力の受け取り
        _h = Input.GetAxisRaw("Horizontal");
        _v = Input.GetAxisRaw("Vertical");

        // アニメーションの処理
        _animator.SetFloat("Horizontal", Mathf.Abs(_h));
        _animator.SetFloat("Vertical", _v);
        _animator.SetFloat("MoveSpeed", _rb.velocity.magnitude);

        // プレイヤーの移動する方向に応じてプレイヤーを反転させる処理
        if (_h > 0)
        {
            this.transform.localScale = new Vector3(-1 * Mathf.Abs(this.transform.localScale.x), this.transform.localScale.y, this.transform.localScale.z);
        }
        else if (_h < 0)
        {
            this.transform.localScale = new Vector3(Mathf.Abs(this.transform.localScale.x), this.transform.localScale.y, this.transform.localScale.z);
        }

        if (_moveSpeed < _maxSpeed / 2)
        {
            _sweat.SetActive(true);
        }
        else
        {
            _sweat.SetActive(false);
        }
    }
    private void FixedUpdate()
    {
        if (_isStun || _pedController.IsOnThePedestal == true || GameManager.Instance.IsPause)
        {
            return;
        }
        Vector2 dir = new Vector2(_h, _v).normalized;
        _rb.velocity = dir * _moveSpeed;
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Item") && _inventory < _inventorySize) // プレイヤーがアイテムに触れたときの処理
        {
            var score = collision.gameObject.GetComponent<LostItemController>().Score; // 触れたアイテムからスコアの値を取得
            _tmpScore += score;
            _inventory++; // 触れたアイテムをリストに格納
            _moveSpeed = Mathf.Clamp(_moveSpeed *= 1 - _inventory / (float)_inventorySize, 1f, _maxSpeed); // アイテムの所持数に応じて移動速度を減らす
            Debug.Log(_moveSpeed);
        }
        else if (collision.gameObject.CompareTag("Koban")) // プレイヤーが交番のコライダーに触れたときの処理
        {
            _gamemanager.ScoreCount(_tmpScore);
            _tmpScore = 0;
            _inventory = 0;
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
        _spriteRenderer.sprite = _stunsprite;
        _isStun = true;
        _rb.velocity = Vector2.zero;
        yield return new WaitForSeconds(time);
        _spriteRenderer.sprite = tmp;
        _isStun = false;
    }
}
