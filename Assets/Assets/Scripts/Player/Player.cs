using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Player : MonoBehaviour, IDamageable
{

    private Rigidbody2D _rigid;
    private SpriteRenderer _spriteRenderer;
    private SpriteRenderer _swordArcSpriteRenderer;
    private float _jumpForce = 6.0f;
    private bool _resetJumpNeeded = false;
    [SerializeField]
    private float _speed = 5.0f;
    [SerializeField]
    protected int health;
    private bool _grounded;

    public int diamonds;
    public int Health { get; set; }
    public bool Death { get; set; }
    public bool inAttackDistance = false;

    private PlayerAnimation _playerAnimation;

    void Start()
    {
        _rigid = GetComponent<Rigidbody2D>();
        _playerAnimation = GetComponent<PlayerAnimation>();
        _spriteRenderer = GetComponentInChildren<SpriteRenderer>();
        _swordArcSpriteRenderer = transform.GetChild(1).GetComponent<SpriteRenderer>();

        Health = health;
    }

    // Update is called once per frame
    void Update()
    {
        if (!Death)
        {
            Movement();
        }

        if(Input.GetMouseButtonDown(0))
        {
            _playerAnimation.Attack();
        }
        UIManager.Instance.HandleToolTip(inAttackDistance);
    }

    void Movement()
    {
        // Capture Horizontal Movement
        float move = Input.GetAxisRaw("Horizontal");
        
        _grounded = IsGrounded();

        // Flip
        if(move < 0)
        {
            _spriteRenderer.flipX = true;
            _swordArcSpriteRenderer.flipY=true;

            Vector3 newPosition = _swordArcSpriteRenderer.transform.localPosition;
            newPosition.x = -1.01f;
            _swordArcSpriteRenderer.transform.localPosition = newPosition;
        }
        else if(move > 0)
        {
            _spriteRenderer.flipX = false;
            _swordArcSpriteRenderer.flipY = false;

            Vector3 newPosition = _swordArcSpriteRenderer.transform.localPosition;
            newPosition.x = 1.01f;
            _swordArcSpriteRenderer.transform.localPosition = newPosition;
        }

        // Jump
        if (Input.GetKeyDown(KeyCode.Space) && IsGrounded())
        {
            _rigid.velocity = new Vector2(_rigid.velocity.x, _jumpForce);
            _resetJumpNeeded = true;
            StartCoroutine(ResetJumpNeededRoutine());
            _playerAnimation.Jump(true);
        }

        _rigid.velocity = new Vector2(move * _speed, _rigid.velocity.y);
        _playerAnimation.Move(move);
    }

    bool IsGrounded()
    {
        RaycastHit2D hitInfo = Physics2D.Raycast(transform.position, Vector2.down, 1.2f, 1 << 8);
        if (hitInfo.collider != null)
        {
            if (_resetJumpNeeded == false)
            {
                _playerAnimation.Jump(false);
                return true;
            }
        }
        return false;
    }

    IEnumerator ResetJumpNeededRoutine()
    {
        yield return new WaitForSeconds(0.1f);
        _resetJumpNeeded = false;
    }

    public void Damage()
    {
        Health--;
        UIManager.Instance.UpdateLives(Health);     
        if (Health < 1)
        {
            _playerAnimation.Death();
            Death = true;
            StartCoroutine(RedirectToMainMenu());
        }
        else { 
        _playerAnimation.Hit();
        } 
    }

    IEnumerator RedirectToMainMenu()
    {
        yield return new WaitForSeconds(5f);
        SceneManager.LoadScene("MainMenu");

    }

    public void AddGems(int amount)
    {
        diamonds += amount;
        UIManager.Instance.UpdateGemCount(diamonds);
    }
}
