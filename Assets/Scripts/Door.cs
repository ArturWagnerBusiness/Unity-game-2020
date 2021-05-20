using System.Security.Cryptography.X509Certificates;
using UnityEngine;

public class Door : MonoBehaviour
{
    public GameObject onDeath;
    public GameObject doorSound;
    public int keysNeeded = 3;

    void Start()
    {

    }

    void Open()
    {
        Instantiate(doorSound, transform.position, Quaternion.identity);
        Instantiate(onDeath, transform.position, transform.rotation);
        Destroy(gameObject);
    }

    public void AddKey()
    {
        keysNeeded -= 1;
        if (keysNeeded == 0)
        {
            Open();
        }
    }
}
