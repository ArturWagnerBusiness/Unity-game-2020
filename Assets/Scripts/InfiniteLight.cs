using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InfiniteLight : MonoBehaviour
{
    public float maxLight = 35f;
    public float rate = 2f;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.name == "Player")
        {
            Player player = collision.gameObject.GetComponent<Player>();
            if (player.lightLevel < maxLight) {
                player.lightLevel += rate * Time.deltaTime;
            }
        }
    }
}
