using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlaySound : MonoBehaviour
{
    public AudioClip pickupSound;
    public AudioSource audioSource;
    // Start is called before the first frame update
    void Start()
    {
        audioSource.PlayOneShot(pickupSound);
        Destroy(gameObject, pickupSound.length);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
