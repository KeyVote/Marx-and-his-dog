using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThwompHandler : MonoBehaviour
{
    private Health health;

    // Start is called before the first frame update
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
        if (collision.CompareTag("Player"))
        {
            health.TakeDamageHuman(2);

        }
        else if (collision.CompareTag("Dog"))
        {
            health.TakeDamageDog(1);
        }
    }
}
