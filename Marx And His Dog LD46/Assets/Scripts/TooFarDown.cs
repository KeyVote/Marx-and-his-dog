using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TooFarDown : MonoBehaviour
{
    // Start is called before the first frame update

    private Health health;

    void Start()
    {
        health = GameObject.FindGameObjectWithTag("Player").GetComponent<Health>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Dog") || collision.CompareTag("Player"))
        {
            health.Die();
        }
    }
}
