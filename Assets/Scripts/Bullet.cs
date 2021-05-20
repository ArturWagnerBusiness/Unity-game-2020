using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public Rigidbody2D rb;
    public float bulletForce = 25f;
    public float lifeSpan = 2f;
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
        if(
        hitInfo.collider.gameObject.layer == 12 ||
        hitInfo.collider.gameObject.layer == 9
        )
        {
            return;
        }

        Gunner gunner = hitInfo.collider.GetComponent<Gunner>();
        if (gunner != null)
        {
            gunner.TakeDamage(dmg);
        }
        Runner runner = hitInfo.collider.GetComponent<Runner>();
        if (runner != null)
        {
            runner.TakeDamage(dmg);
        }
        Defender defender = hitInfo.collider.GetComponent<Defender>();
        if (defender != null)
        {
            defender.TakeDamage(dmg);
        }

        Instantiate(afterObject, transform.position, transform.rotation);
        Destroy(gameObject);
    }
}
