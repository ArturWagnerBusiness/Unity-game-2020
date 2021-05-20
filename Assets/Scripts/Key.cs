using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Key : MonoBehaviour
{
    public Door[] doors;
    public GameObject keySound;
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
            Instantiate(keySound, transform.position, Quaternion.identity);
            foreach (Door door in doors)
            {
                door.AddKey();
            }
            Destroy(gameObject);
        }
    }
}
