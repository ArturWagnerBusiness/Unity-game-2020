using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Defender : MonoBehaviour
{
    public float respawnTime = 20f;
    Vector3 spawnPoint;
    public int maxHealth = 20;
    public float speed = 15f;
    public float stopDistance = 10f;
    [Space()]
    public Sprite[] gunHeatLevels;
    public SpriteRenderer gunSpriteRenderer;
    [Space()]
    public Rigidbody2D rbGun;
    int health;

    public Vector3 goal;

    Vector2 moveTowards;
    Vector2 moveVelocity;
    Vector2 nextPoint;
    Rigidbody2D rb;
    bool inRange = false;
    bool updateSources = true;
    float timeTillNextPoint = 0f;
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

        gunSpriteRenderer.sprite = gunHeatLevels[(int)((gunHeatLevels.Length - 1) * ((float)health / maxHealth))];
        if (!updateSources)
        {

            inRange = Vector2.Distance(transform.position, moveTowards) < stopDistance;

            Vector2 direction = (Vector2)transform.position - moveTowards;
            float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
            rb.rotation = angle;
            direction.Normalize();
            moveVelocity = direction;
        }
        else
        {
            inRange = false;
            if (timeTillNextPoint < Time.time || Vector2.Distance(transform.position, nextPoint) < stopDistance)
            {
                timeTillNextPoint = Time.time + 5f;
                nextPoint = (Vector2)transform.position - new Vector2(Random.Range(-100.0f, 100.0f), Random.Range(-100.0f, 100.0f));
            }
            float angle = Mathf.Atan2(nextPoint.y, nextPoint.x) * Mathf.Rad2Deg;
            rb.rotation = angle;
            moveVelocity = nextPoint.normalized;
        }
    }
    private void FixedUpdate()
    {
        if (!inRange)
        {
            rb.MovePosition((Vector2)transform.position - moveVelocity * speed * Time.deltaTime);
        }
        rbGun.MovePosition((Vector2)transform.position - moveVelocity * speed * Time.deltaTime);
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
        gameObject.SetActive(true);

    }
    public void TakeDamage(int dmg)
    {
        health -= dmg;
    }
    void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Light Bulb") || collision.CompareTag("Key"))
        {
            updateSources = true;
            timeTillNextPoint = Time.time + 5f;
            nextPoint = (Vector2)transform.position - new Vector2(Random.Range(-100.0f, 100.0f), Random.Range(-100.0f, 100.0f));
            moveTowards = new Vector2(999999f, 999999f);
        }
    }
    void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.CompareTag("Light Bulb") || collision.CompareTag("Key"))
        {
            updateSources = false;
            Transform objectPosition = collision.gameObject.GetComponent<Transform>();
            Debug.Log(Vector2.Distance(transform.position, objectPosition.position));
            if (Vector2.Distance(transform.position, objectPosition.position) < Vector2.Distance(transform.position, moveTowards))
            {
                moveTowards = objectPosition.position;
            }
        }
    }
}
