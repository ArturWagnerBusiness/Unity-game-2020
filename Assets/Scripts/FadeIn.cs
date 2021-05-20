using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FadeIn : MonoBehaviour
{
    public Image image;
    public float loadingTime = 1f;
    public bool destroy = true;
    Color color = new Color(0f, 0f, 0f);
    float colorVal = 1f;
    bool isFadeout = false;
    void Start()
    {
        image.color = color;
        isFadeout = true;
    }
    void Update()
    {
        if (isFadeout)
        {
            if (colorVal > 0f)
            {
                colorVal -= Time.deltaTime / loadingTime;
                color.a = colorVal;
                image.color = color;
            }
            else
            {
                if (destroy)
                {
                    Destroy(gameObject);
                }
                else
                {
                    isFadeout = false;
                }
            }
        }
    }
}
