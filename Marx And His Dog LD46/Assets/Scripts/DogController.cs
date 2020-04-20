using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DogController : MonoBehaviour
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

    private float distanceBtwnPlayerAndDog;
    private bool tooCloseOrFar;
    private Vector3 lookDirection;

    //public GameObject player;

    private Animator anim;

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
        health = GameObject.FindGameObjectWithTag("Player").GetComponent<Health>();
    }

    // Update is called once per frame
    void Update()
    {
        if (transform.position.y < -100)
        {
            health.Die();
        }

        /*distanceBtwnPlayerAndDog = (player.transform.position - transform.position).sqrMagnitude;
        lookDirection = (player.transform.position - transform.position).normalized;

        Debug.Log(distanceBtwnPlayerAndDog);
        if (distanceBtwnPlayerAndDog > 8f && isGrounded() == false || distanceBtwnPlayerAndDog < 4f && isGrounded() == false)
        {
            tooCloseOrFar = true;
        }
        else
        {
            tooCloseOrFar = false;
        }*/

        float moveX = 0f;
        float moveY = 0f;

        if (Input.GetKeyDown(KeyCode.Space) && isGrounded())
        {
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
        //tooCloseOrFar = true;
        moveInput = Input.GetAxisRaw("Horizontal");
        rigidbody2D.velocity = new Vector2(moveDir.x * speed * Time.deltaTime, rigidbody2D.velocity.y);

        if (moveInput == 0)
        {
            anim.SetBool("isDogRunning", false);
        }
        else
        {
            anim.SetBool("isDogRunning", true);
        }

        if (moveInput < 0)
        {
            transform.eulerAngles = new Vector3(0, 180, 0);
        }
        else if (moveInput > 0)
        {
            transform.eulerAngles = new Vector3(0, 0, 0);
        }

        //If I come up with a good idea to keep the dog behind me I will try to fix it

        /*if (tooCloseOrFar == false)
        {
            rigidbody2D.velocity = new Vector2(moveDir.x * speed * Time.deltaTime, rigidbody2D.velocity.y); //moveDir eller moveInput?!?!? :O
            Debug.Log("FAlse");
        }
        else
        {
            //lookDirection.x -= 2;
            rigidbody2D.MovePosition(new Vector2(transform.position.x + lookDirection.x * Time.deltaTime * speed, transform.position.y));
            Debug.Log("TrueE");
           // rigidbody2D.MovePosition(new Vector2((player.transform.position.x - 2) * moveInput * speed * Time.deltaTime, transform.position.y));
        }*/

        /*if (isJumpButtonDown)
        {
            rigidbody2D.velocity = Vector2.up * jumpPower * Time.deltaTime;
            isJumpButtonDown = false;
        }*/
    }

    private bool isGrounded()
    {
        RaycastHit2D rayCastHit2D = Physics2D.BoxCast(boxCollider2D.bounds.center, boxCollider2D.size, 0f, Vector2.down, 0.1f, groundLayerMask);
        Debug.Log(rayCastHit2D.collider);
        return rayCastHit2D.collider != null;
    }
}
