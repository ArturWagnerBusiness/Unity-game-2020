using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gunner : MonoBehaviour
{
    public float respawnTime = 20f;
    Vector3 spawnPoint;
    public int maxHealth = 100;
    public float speed = 10f;
    public float stopDistance = 10f;
    [Space()]
    public float shotsPerSecond = 0.5f;
    public Transform firePoint;
    public GameObject bulletPrefab;
    public AudioClip shotSound;
    [Space()]
    public Sprite[] gunHeatLevels;
    public SpriteRenderer gunSpriteRenderer;
    
    int health;
    Vector2 moveTowards;
    Vector2 moveVelocity;
    Rigidbody2D rb;
    bool attack = false;
    bool inRange = false;
    float nextTimeToFire = 0;
    private AudioSource audioSource;
    // Start is called before the first frame update
    void Start()
    {
        audioSource = gameObject.AddComponent<AudioSource>();
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
        if (attack)
        {

            if (Time.time > nextTimeToFire)
            {
                nextTimeToFire = Time.time + 1f / shotsPerSecond;
                Shoot();
            }
            inRange = Vector2.Distance(transform.position, moveTowards) < stopDistance;
         
            Vector2 direction = (Vector2)transform.position - moveTowards;
            float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
            rb.rotation = angle;
            direction.Normalize();
            moveVelocity = direction;
        } else
        {
            //patroll script
        }


    }
    void Shoot()
    {
        audioSource.PlayOneShot(shotSound);
        Instantiate(bulletPrefab, firePoint.position, firePoint.rotation * Quaternion.Euler(0, 0, 90));
    }
    private void FixedUpdate()
    {
        if(attack && !inRange)
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
        gameObject.SetActive(true);

    }
    public void TakeDamage(int dmg)
    {
        health -= dmg;
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
