using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBullet : MonoBehaviour
{
    public Rigidbody2D rb;
    public float bulletForce = 20f;
    public float lifeSpan = 1.5f;
    public int dmg = 1;
    public GameObject afterObject;

    // Start is called before the first frame update
    void Start()
    {
        rb.velocity = transform.right * bulletForce;
        Destroy(gameObject, lifeSpan);
    }

    // Update is called once per frame
    void OnCollisionEnter2D(Collision2D hitInfo)
    {
        //Debug.Log(hitInfo.collider.gameObject.layer);
        if (
        hitInfo.collider.gameObject.layer == 12 ||
        hitInfo.collider.gameObject.layer == 9
        )
        {
            return;
        }

        Player player = hitInfo.collider.GetComponent<Player>();
        if (player != null)
        {
            player.TakeDamage(dmg);
        }

        Instantiate(afterObject, transform.position, transform.rotation);
        Destroy(gameObject);
    }
}
