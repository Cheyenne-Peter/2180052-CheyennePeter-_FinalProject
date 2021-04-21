using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MenuController : MonoBehaviour
{
    public GameObject howtoPanel;
    public bool gameisPaused;

    Scene currentScene;
    public void Start()
    {
        currentScene = SceneManager.GetActiveScene();
        if (currentScene.name == "Stage1")
        {
            howtoPanel.SetActive(true);
            Time.timeScale = 0f;
            gameisPaused = true;
        }
        else if (currentScene.name == "Stage2")
        {
            howtoPanel.SetActive(true);
            Time.timeScale = 0f;
            gameisPaused = true;
        }
        else if (currentScene.name == "Stage3")
        {
            howtoPanel.SetActive(true);
            Time.timeScale = 0f;
            gameisPaused = true;
        }


    }
    public void QuitGame()
    {
        Application.Quit();
    }

    public void MainMenu()
    {
        SceneManager.LoadScene("MainMenu");
    }

    public void PlayGame()
    {
        SceneManager.LoadScene("Stage1");
    }

    public void ExitPanel()
    {
        howtoPanel.SetActive(false);
        Time.timeScale = 1f;
        gameisPaused = false;
    }

}
