using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private LayerMask groundLayerMask;
    public float speed;
    public float jumpPower;
    private Rigidbody2D rigidbody2D;
    private Vector3 moveDir;
    private BoxCollider2D boxCollider2D;
    private float moveInput;

    private float jumpTimeCounter;
    public float jumpTime;
    private bool isJumping;

    private Animator anim;

    public static AudioClip jumpSound;
    public AudioSource audioSource;

    private Health health;

    // Start is called before the first frame update
    void Awake()
    {
        anim = GetComponent<Animator>();
        rigidbody2D = GetComponent<Rigidbody2D>();
        boxCollider2D = GetComponent<BoxCollider2D>();
    }

    private void Start()
    {
        jumpSound = Resources.Load<AudioClip>("Jump");
        health = GameObject.FindGameObjectWithTag("Player").GetComponent<Health>();
    }

    // Update is called once per frame
    void Update()
    {
        if (transform.position.y < -100)
        {
            health.Die();
        }

        float moveX = 0f;
        float moveY = 0f;

        if (isGrounded())
        {
            anim.SetBool("isJumping", false);
            Debug.Log("Is grounded");
        } else
        {
            anim.SetBool("isJumping", true);
        }

        if (Input.GetKeyDown(KeyCode.Space) && isGrounded())
        {
            audioSource.PlayOneShot(jumpSound);
            anim.SetBool("isJumping", true);
            isJumping = true;
            jumpTimeCounter = jumpTime;
            rigidbody2D.velocity = Vector2.up * jumpPower * Time.deltaTime;
        }

        if (Input.GetKey(KeyCode.Space) && isJumping == true)
        {
            if (jumpTimeCounter > 0)
            {
                rigidbody2D.velocity = Vector2.up * jumpPower * Time.deltaTime;
                jumpTimeCounter -= Time.deltaTime;
            } else
            {
                isJumping = false;
            }
        }

        if (Input.GetKeyUp(KeyCode.Space))
        {
            isJumping = false;
        }

        if (Input.GetKey(KeyCode.A))
        {
            moveX = -1f;
        }

        if (Input.GetKey(KeyCode.D))
        {
            moveX = +1f;
        }

        moveDir = new Vector3(moveX, moveY).normalized;
    }

    private void FixedUpdate()
    {
        moveInput = Input.GetAxisRaw("Horizontal");
        rigidbody2D.velocity = new Vector2(moveDir.x * speed * Time.deltaTime, rigidbody2D.velocity.y);

        if (moveInput == 0)
        {
            anim.SetBool("isWalking", false);
        }
        else
        {
            anim.SetBool("isWalking", true);
        }

        if (moveInput < 0)
        {
            transform.eulerAngles = new Vector3(0, 180, 0);
        } 
        else if (moveInput > 0)
        {
            transform.eulerAngles = new Vector3(0, 0, 0);
        }
    }

    private bool isGrounded()
    {
        RaycastHit2D rayCastHit2D = Physics2D.BoxCast(boxCollider2D.bounds.center, boxCollider2D.size, 0f, Vector2.down, 0.1f, groundLayerMask);
        Debug.Log(rayCastHit2D.collider);
        return rayCastHit2D.collider != null;
    }
}
