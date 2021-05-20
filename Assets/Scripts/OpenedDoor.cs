using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class OpenedDoor : MonoBehaviour
{
    // Start is called before the first frame update
    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.name == "Player")
        {
            if (SceneManager.GetActiveScene().buildIndex == 1)
            {
                SceneManager.LoadScene(0);
            }
            else if (SceneManager.GetActiveScene().buildIndex == 4)
            {
                SceneManager.LoadScene(0);
            }
            else
            {
                SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
            }
        }
    }
}
