using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spikes : MonoBehaviour
{
    private Health health;

    public static AudioClip hurtSound;
    public AudioSource audioSource;

    // Start is called before the first frame update
    void Start()
    {
        health = GameObject.FindGameObjectWithTag("Player").GetComponent<Health>();
        hurtSound = Resources.Load<AudioClip>("Hurt");
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            audioSource.PlayOneShot(hurtSound);
            health.TakeDamageHuman(1);

            StartCoroutine(health.TakeKnockback(0.02f, 7500, health.transform.position, false));
        } 
        else if (collision.CompareTag("Dog"))
        {
            audioSource.PlayOneShot(hurtSound);
            health.TakeDamageDog(1);

            StartCoroutine(health.TakeKnockback(0.02f, 7500, health.dog.transform.position, true));
        }
    }
}
