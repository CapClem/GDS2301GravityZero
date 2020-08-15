using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class ButtonFunctions : MonoBehaviour
{
    bool vidPlaying = false;
    Animator buttonsAni;
    public GameObject buttons;

    void Start()
    {
        buttonsAni = buttons.GetComponent<Animator>();
    }

    void Update()
    {
        //reload scene
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            int scene = SceneManager.GetActiveScene().buildIndex;
            SceneManager.LoadScene(scene, LoadSceneMode.Single);
            Time.timeScale = 1;
        }

        //quit area
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (SceneManager.GetActiveScene().name == "MainMenu" && vidPlaying == true)
            {
                LoadGame();
            }
            else if (SceneManager.GetActiveScene().name == "MainMenu" && vidPlaying == false)
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

    public void StartIntro()
    {
        buttonsAni.SetTrigger("LoadButtonsOut");
        Animator y = this.GetComponent<Animator>();
        y.SetBool("Start Video", true);

        GameObject x = GameObject.Find("Video Pannel");
        Animator ani = x.GetComponent<Animator>();
        ani.SetTrigger("Start Intro");
        vidPlaying = true;
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
