using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private float _moveSpeed;
    [SerializeField] private float _jumpForce;

    [SerializeField] private Rigidbody2D _rb;
    [SerializeField] private Transform _groundCheck;

    [SerializeField] private bool _isGrounded;
    [SerializeField] private bool _isJumping;

    public LayerMask whatIsGround;

    public Animator anim;

    public BulletController shotToFire;
    public Transform shotPoint;

    [SerializeField] private bool _canDoubleJump;

    [SerializeField] private float _coyoteTime = 0.2f;
    [SerializeField] private float _coyoteTimeCounter;

    [SerializeField] private float _jumpBufferTime = 0.2f;
    [SerializeField] private float _jumpBufferCounter;

    // Start is called before the first frame update
    void Start()
    {
        Rigidbody2D rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        CharacterMove();

        PlayerFire();
    }

    void CharacterMove()
    {
        _rb.velocity = new Vector2(Input.GetAxisRaw("Horizontal") * _moveSpeed, _rb.velocity.y);

        if(_isGrounded == true)
        {
            _coyoteTimeCounter = _coyoteTime;
        }
        else
        {
            _coyoteTimeCounter -= Time.deltaTime;
        }

        if (Input.GetButtonDown("Jump"))
        {
            _jumpBufferCounter = _jumpBufferTime;
        }
        else
        {
            _jumpBufferCounter -= Time.deltaTime;
        }

        if (_rb.velocity.x < 0)
        {
            transform.localScale = new Vector3(-1, 1f, 1f);
        }
        else if(_rb.velocity.x > 0)
        {
            transform.localScale = Vector3.one;
        }

        _isGrounded = Physics2D.OverlapCircle(_groundCheck.position, .2f, whatIsGround);

        if(_coyoteTimeCounter > 0f && _jumpBufferCounter > 0f && !_isJumping || _canDoubleJump == true && _coyoteTime > 0f && _jumpBufferCounter > 0f)
        {
            if (_isGrounded)
            {
                _canDoubleJump = true;
            }
            else
            {
                _canDoubleJump = false;

                anim.SetTrigger("doubleJump");
            }
            
            _jumpBufferCounter = 0f;

            StartCoroutine(JumpCooldown());
            _rb.velocity = new Vector2(_rb.velocity.x, _jumpForce);
        }
        
        anim.SetBool("IsGrounded", _isGrounded);
        anim.SetFloat("speed", Mathf.Abs(_rb.velocity.x));
    }

    void PlayerFire()
    {
        if(Input.GetButtonDown("Fire1"))
        {
            Instantiate(shotToFire, shotPoint.position, shotPoint.rotation)._moveDir = new Vector2(transform.localScale.x, 0);

            anim.SetTrigger("shotFired");
        }
    }
    private IEnumerator JumpCooldown()
    {
        _isJumping = true;
        yield return new WaitForSeconds(0.4f);
        _isJumping = false;
    }
}
