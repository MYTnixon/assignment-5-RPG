using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public void Play()
    {
        SceneManager.LoadScene(1);
    }

    public void Back()
    {
        SceneManager.LoadScene(0);
    }

    public void Controls()
    {
        SceneManager.LoadScene(3);
    }

    public void Quit()
    {
        Debug.Log("Shutting Down.....");
        Application.Quit();
    }
}
