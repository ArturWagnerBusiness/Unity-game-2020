using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Runner : MonoBehaviour
{
    public float respawnTime = 20f;
    Vector3 spawnPoint;
    public int maxHealth = 5;
    public float speed = 40f;
    public int dmg = 6;
    public GameObject legTopLeft;
    public GameObject legTopRight;
    public GameObject legBottomLeft;
    public GameObject legBottomRight;

    int health;
    Vector2 moveTowards;
    Vector2 moveVelocity;
    Rigidbody2D rb;
    bool attack = false;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        health = maxHealth;
        spawnPoint = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        rb.velocity = Vector2.zero;
        if (health <= 0)
            Die();

        if (attack)
        {

            Vector2 direction = (Vector2)transform.position - moveTowards;
            float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
            rb.rotation = angle;
            direction.Normalize();
            moveVelocity = direction;
        }
        else
        {
            //patroll script
        }


    }
    private void FixedUpdate()
    {
        if (attack)
            rb.MovePosition((Vector2)transform.position - moveVelocity * speed * Time.deltaTime);
    }
    void Die()
    {
        gameObject.SetActive(false);
        Invoke("Respawn", respawnTime);
    }
    void Respawn()
    {
        health = maxHealth;
        transform.position = spawnPoint;
        TakeDamage(0);
        gameObject.SetActive(true);

    }
    public void TakeDamage(int dmgTaken)
    {
        health -= dmgTaken;
        if ((float)health / maxHealth < 0.3f)
        {

            legTopLeft.SetActive(false);
            legTopRight.SetActive(false);
            legBottomLeft.SetActive(false);
            legBottomRight.SetActive(false);
        } else if ((float)health / maxHealth < 0.7f)
        {

            legTopLeft.SetActive(true);
            legTopRight.SetActive(true);
            legBottomLeft.SetActive(false);
            legBottomRight.SetActive(false);
        }
        else
        {
            legTopLeft.SetActive(true);
            legTopRight.SetActive(true);
            legBottomLeft.SetActive(true);
            legBottomRight.SetActive(true);
        }
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.collider.name == "Player")
        {
            Player player = collision.collider.GetComponent<Player>();
            player.TakeDamage(dmg);
        }
    }
    void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.name == "Player")
        {
            attack = false;
        }
    }
    void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.name == "Player")
        {
            attack = true;
            Transform playerPosition = collision.gameObject.GetComponent<Transform>();

            moveTowards = playerPosition.position;
        }
    }
}
