using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DefenderGun : MonoBehaviour
{
    public float shotsPerSecond = 0.5f;
    public Transform firePoint;
    public GameObject bulletPrefab;
    public Rigidbody2D rb;
    public AudioClip shotSound;

    Vector2 fireAtPoint;
    bool attack = false;
    float nextTimeToFire = 0;
    private AudioSource audioSource;
    // Start is called before the first frame update
    void Start()
    {
        audioSource = gameObject.AddComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        Vector2 direction = (Vector2)transform.position - fireAtPoint;
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        rb.rotation = angle;
        if(attack && Time.time > nextTimeToFire)
        {
            nextTimeToFire = Time.time + 1f / shotsPerSecond;
            Shoot();
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if(collision.name == "Player")
        {
            attack = false;
        }
    }
    void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.name == "Player")
        {
            attack = true;
            Transform objectPosition = collision.gameObject.GetComponent<Transform>();
            fireAtPoint = objectPosition.position;
        }
    }
    void Shoot()
    {
        audioSource.PlayOneShot(shotSound);
        Instantiate(bulletPrefab, firePoint.position, firePoint.rotation * Quaternion.Euler(0, 0, 90));
    }
}
