
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour
{
    public Image image;
    public float loadingTime = 1f;
    Color color = new Color(0f, 0f, 0f);
    float colorVal = 0f;
    bool isFadeoutTutorial = false;
    bool isFadeoutGame = false;
    public void startTutorial()
    {
        if (isFadeoutGame)
            return;
        image.gameObject.SetActive(true);
        image.color = color;
        isFadeoutTutorial = true;
    }
    void Update()
    {
        if (isFadeoutTutorial)
        {
            if(colorVal < 1f)
            {
                colorVal += Time.deltaTime / loadingTime;
                color.a = colorVal;
                image.color = color;
            }
            else
            {
                SceneManager.LoadScene(1);
            }
        } else if (isFadeoutGame)
        {
            if (colorVal < 1f)
            {
                colorVal += Time.deltaTime / loadingTime;
                color.a = colorVal;
                image.color = color;
            }
            else
            {
                SceneManager.LoadScene(2);
            }
        }
    }
    public void startGame()
    {
        if (isFadeoutTutorial)
            return;
        isFadeoutGame = true;
        image.gameObject.SetActive(true);
        image.color = color;
    }
    public void QuitGame()
    {
        Application.Quit();
    }
}
