using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public void PlayGame()
    {
        SceneManager.LoadScene("GameScene"); // This will load to the scene called "GamePlay".
    }

    public void instructions()
    {
        SceneManager.LoadScene("Instructions");
    }

    public void QuitButton()
    {
        Application.Quit();
    }

    public void mainMenu()
    {
        SceneManager.LoadScene("MainMenu");
    }

    public void Credit()
    {
        SceneManager.LoadScene("Credit");
    }
}
