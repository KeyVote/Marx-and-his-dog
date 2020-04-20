using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Thwomp : MonoBehaviour
{
    private Health health;
    public float speed;
    public GameObject thwomp;
    private Vector3 orgPos;
    public bool goingDown;
    public bool goingUp;
    public GameObject endDest;

    // Start is called before the first frame update
    void Start()
    {
        health = GameObject.FindGameObjectWithTag("Player").GetComponent<Health>();
        orgPos = thwomp.transform.position;
    }

    private void FixedUpdate()
    {
        if (goingDown == true)
        {
            if (thwomp.transform.position.y == endDest.transform.position.y)
            {
                goingDown = false;
                goingUp = true;
            }
            thwomp.transform.position = Vector2.MoveTowards(thwomp.transform.position, endDest.transform.position, Time.deltaTime * speed);
        }

        if (goingUp == true)
        {
            if (thwomp.transform.position.y == orgPos.y)
            {
                goingUp = false;
            }
            thwomp.transform.position = Vector2.MoveTowards(thwomp.transform.position, orgPos, Time.deltaTime * speed);
        }
    }

    public IEnumerator ThwompDown(float thwompDur)
    {
        float timer = 0;
        while (thwompDur > timer)
        {
            timer += Time.deltaTime;

            thwomp.transform.Translate(Vector2.down * speed * Time.deltaTime);
        }

        yield return 0;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player") && goingUp == false)
        {
            goingDown = true;
        }
        else if (collision.CompareTag("Dog") && goingUp == false)
        {
            goingDown = true;
        }
    }
}
