using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement; 

public class Health : MonoBehaviour
{
    public int dogHealth;
    public int numOfDogHearts;
    public int health;
    public int numOfHearts;

    public Image[] hearts;
    public Image[] dogHearts;
    public Sprite fullDogHeart;
    public Sprite fullHeart;
    public Sprite emptyHeart;

    private Rigidbody2D rigidbody2D;
    private Rigidbody2D dogRigidBody2D;
    public GameObject dog;

    // Start is called before the first frame update
    void Awake()
    {
        rigidbody2D = GetComponent<Rigidbody2D>();
        dogRigidBody2D = dog.GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        if (health > numOfHearts)
        {
            health = numOfHearts;
        }

        if (dogHealth > numOfDogHearts)
        {
            dogHealth = numOfDogHearts;
        }

        for (int i = 0; i < hearts.Length; i++)
        {
            if (i < health)
            {
                hearts[i].sprite = fullHeart;
            } else
            {
                hearts[i].sprite = emptyHeart;
            }

            if (i < numOfHearts)
            {
                hearts[i].enabled = true;
            } else
            {
                hearts[i].enabled = false;
            }
        }

        for (int i = 0; i < dogHearts.Length; i++)
        {
            if (i < dogHealth)
            {
                dogHearts[i].sprite = fullDogHeart;
            } else
            {
                dogHearts[i].sprite = emptyHeart;
            }

            if (i < numOfDogHearts)
            {
                dogHearts[i].enabled = true;
            } else
            {
                dogHearts[i].enabled = false;
            }
        }
    }

    public void Die()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void TakeDamageHuman(int damage)
    {
        health -= damage;

        if (health < 1)
        {
            Die();
        }
    }

    public void TakeDamageDog(int damage)
    {
        dogHealth -= damage;

        if (dogHealth < 1)
        {
            Die();
        }
    }

    public IEnumerator TakeKnockback(float knockDur, float knockbackPwr, Vector3 knockbackDir, bool isDog)
    {
        float timer = 0;
        if (isDog == false)
        {
            while (knockDur > timer)
            {
                timer += Time.deltaTime;

                rigidbody2D.AddForce(new Vector3(knockbackDir.x * -1000, knockbackDir.y + knockbackPwr, transform.position.z));
            }
        }
        else
        {
            timer += Time.deltaTime;

            dogRigidBody2D.AddForce(new Vector3(knockbackDir.x * -1000, knockbackDir.y + knockbackPwr, transform.position.z));
        }

        yield return 0;
    }
}
