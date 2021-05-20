using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonClick : MonoBehaviour
{
    // Start is called before the first frame update
    public AudioClip pickupSound;
    public AudioSource audioSource;
    // Start is called before the first frame update
    public void PlayClickSound()
    {
        audioSource.PlayOneShot(pickupSound);
    }
}
