using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private float _moveSpeed;
    [SerializeField] private float _jumpForce;

    [SerializeField] private Rigidbody2D _rb;
    [SerializeField] private Transform _groundCheck;

    private bool _isGrounded;

    public LayerMask whatIsGround;

    public Animator anim;

    // Start is called before the first frame update
    void Start()
    {
        Rigidbody2D rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        CharacterMove();

        
    }

    void CharacterMove()
    {
        _rb.velocity = new Vector2(Input.GetAxisRaw("Horizontal") * _moveSpeed, _rb.velocity.y);

        if(_rb.velocity.x < 0)
        {
            transform.localScale = new Vector3(-1, 1f, 1f);
        }
        else if(_rb.velocity.x > 0)
        {
            transform.localScale = Vector3.one;
        }

        _isGrounded = Physics2D.OverlapCircle(_groundCheck.position, .2f, whatIsGround);

        if(Input.GetButtonDown("Jump") && _isGrounded == true)
        {
            _rb.velocity = new Vector2(_rb.velocity.x, _jumpForce);
        }

        anim.SetBool("IsGrounded", _isGrounded);
        anim.SetFloat("speed", Mathf.Abs(_rb.velocity.x));
    }
}
