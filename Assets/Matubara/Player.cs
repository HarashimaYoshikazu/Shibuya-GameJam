using System.Collections;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]

public class Player : MonoBehaviour
{
    [SerializeField, Header("�v���C���[�̍ő呬�x")] float _maxSpeed;
    [SerializeField, Header("�v���C���[�����Ƃ��������Ă�ő吔")] int _inventorySize;
    [SerializeField, Header("��J���ɕ\������摜�̃Q�[���I�u�W�F�N�g")] GameObject _sweat;
    Rigidbody2D _rb;
    float _h;
    float _v;
    /// <summary> ���Ƃ������E��������ێ����Ă����ϐ� </summary>
    float _inventory;
    float _moveSpeed;
    GameManager _gamemanager;
    bool _isStun;
    PedestalController _pedController;
    [SerializeField, Header("�X�^�������Ƃ��ɐ؂�ւ���摜")] Sprite _stunsprite;
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
        // ���͂̎󂯎��
        _h = Input.GetAxisRaw("Horizontal");
        _v = Input.GetAxisRaw("Vertical");

        // �A�j���[�V�����̏���
        _animator.SetFloat("Horizontal", Mathf.Abs(_h));
        _animator.SetFloat("Vertical", _v);
        _animator.SetFloat("MoveSpeed", _rb.velocity.magnitude);

        // �v���C���[�̈ړ���������ɉ����ăv���C���[�𔽓]�����鏈��
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
        if (collision.gameObject.CompareTag("Item") && _inventory < _inventorySize) // �v���C���[���A�C�e���ɐG�ꂽ�Ƃ��̏���
        {
            var score = collision.gameObject.GetComponent<LostItemController>().Score; // �G�ꂽ�A�C�e������X�R�A�̒l���擾
            _tmpScore += score;
            _inventory++; // �G�ꂽ�A�C�e�������X�g�Ɋi�[
            _moveSpeed = Mathf.Clamp(_moveSpeed *= 1 - _inventory / (float)_inventorySize, 1f, _maxSpeed); // �A�C�e���̏������ɉ����Ĉړ����x�����炷
            Debug.Log(_moveSpeed);
        }
        else if (collision.gameObject.CompareTag("Koban")) // �v���C���[����Ԃ̃R���C�_�[�ɐG�ꂽ�Ƃ��̏���
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
