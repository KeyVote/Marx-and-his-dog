using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Patrol : MonoBehaviour
{
    public float speed;
    public float distance;

    private bool movingRight = true;

    public Transform groundDetection;

    private Health health;

    // Start is called before the first frame update
    // Update is called once per frame
    private void Start()
    {
        health = GameObject.FindGameObjectWithTag("Player").GetComponent<Health>();
    }

    private void FixedUpdate()
    {

        transform.Translate(Vector2.right * speed * Time.deltaTime);

        RaycastHit2D groundInfo = Physics2D.Raycast(groundDetection.position, Vector2.down, 2f);
        if (groundInfo.collider == false)
        {
            if (movingRight == true)
            {
                transform.eulerAngles = new Vector3(0, -180, 0);
                movingRight = false;
            }
            else
            {
                transform.eulerAngles = new Vector3(0, 0, 0);
                movingRight = true;
            }
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            //if (collision.gameObject.GetComponent<Rigidbody2D>().velocity.y > 0)
            health.TakeDamageHuman(2);
        }

        if (collision.gameObject.tag == "Dog")
        {
            health.TakeDamageDog(1);
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player") || other.CompareTag("Dog"))
        {
            if (other.GetComponent<Rigidbody2D>().velocity.y <= 0f) {
                if (other.CompareTag("Player"))
                {
                    other.GetComponent<Rigidbody2D>().velocity = new Vector2(other.GetComponent<Rigidbody2D>().velocity.x, 5);
                    Destroy(gameObject);
                }
                else
                {
                    other.GetComponent<Rigidbody2D>().velocity = new Vector2(other.GetComponent<Rigidbody2D>().velocity.x, 5);
                    Destroy(gameObject);
                }
            }
        }
    }
}

