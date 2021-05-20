using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletDestroy : MonoBehaviour
{
    public AudioClip clip;

    [Range(0f, 1f)]
    public float volume;
    [Range(.1f, 3f)]
    public float pitch;

    public bool loop;

    [HideInInspector]
    public AudioSource source;
    // Start is called before the first frame update
    void Start()
    {
        source = gameObject.AddComponent<AudioSource>();

        source.clip = clip;
        source.volume = volume;
        source.pitch = pitch;
        source.Play();
        Destroy(gameObject, 0.3f);
    }
}
