using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class ButtonFunctions : MonoBehaviour
{
    
    
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            int scene = SceneManager.GetActiveScene().buildIndex;
            SceneManager.LoadScene(scene, LoadSceneMode.Single);
            Time.timeScale = 1;
        }

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (SceneManager.GetActiveScene().name == "MainMenu")
            {
                ExitGame();
            }
            else if (SceneManager.GetActiveScene().name != "MainMenu" && SceneManager.GetActiveScene().name != "Controls" && SceneManager.GetActiveScene().name != "Contributions")
            {
                //ReturnToMainMenu();
                LoadMainMenu();
            }    
            else
            {
                LoadMainMenu();
            }
        }
    }

    public void LoadGame()
    {
        SceneManager.LoadScene("Level_1", LoadSceneMode.Single);
    }

    public void LoadControls()
    {
        SceneManager.LoadScene("Controls", LoadSceneMode.Single);
    }

    public void LoadContributions()
    {
        SceneManager.LoadScene("Contributions", LoadSceneMode.Single);
    }

    public void ExitGame()
    {
        Application.Quit();
    }

    public void ReturnToMainMenu()
    {
        SceneManager.LoadScene("MainMenu", LoadSceneMode.Additive);
    }
    
    public void LoadMainMenu()
    {
        SceneManager.LoadScene("MainMenu");
    }

}
