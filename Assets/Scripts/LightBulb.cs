using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightBulb : MonoBehaviour
{
    public float lightLevel = 7f;
    public float respawnTime = 30f;
    public GameObject pickupSound;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.name == "Player")
        {
            Player player = collision.gameObject.GetComponent<Player>();
            player.lightLevel += lightLevel;
            Instantiate(pickupSound, transform.position, Quaternion.identity);
            gameObject.SetActive(false);
            Invoke("Respawn", respawnTime);
        }
    }
    void Respawn()
    {
        gameObject.SetActive(true);

    }
}
