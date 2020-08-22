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

    bool gamePaused = false;

    Animator AniGameplayButton;
    Animator AniGameplayFader;
    Animator AniHowToPlay;

    void Awake()
    {
        if (SceneManager.GetActiveScene().name != "MainMenu" && SceneManager.GetActiveScene().name != "HowToPlay" && SceneManager.GetActiveScene().name != "Settings" && SceneManager.GetActiveScene().name != "Credits" && SceneManager.GetActiveScene().name != "EndScene")
        {
            Cursor.visible = false;
        }
        else
        {
            Cursor.visible = true;
        }
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
                F_MainMenu.animaticMusic.stop(FMOD.Studio.STOP_MODE.ALLOWFADEOUT);
                LoadGame();
            }
            else if (SceneManager.GetActiveScene().name != "MainMenu" && SceneManager.GetActiveScene().name != "HowToPlay" && SceneManager.GetActiveScene().name != "Settings" && SceneManager.GetActiveScene().name != "Credits" && SceneManager.GetActiveScene().name != "EndScene")
            {              
                AniGameplayFader = GameObject.FindGameObjectWithTag("PlayerFade").GetComponent<Animator>();
                AniGameplayButton = GameObject.FindGameObjectWithTag("PlayerButtons").GetComponent<Animator>();
                AniHowToPlay = GameObject.FindGameObjectWithTag("PlayerControls").GetComponent<Animator>();

                if (gamePaused == false)
                {
                    FMODUnity.RuntimeManager.PlayOneShot("event:/Ui/MenuOpen", default);
                    AniGameplayButton.SetTrigger("FadeIn");
                    AniGameplayFader.SetTrigger("FadeIn");
                    AniGameplayButton.SetBool("FaderBool", true);
                    AniGameplayFader.SetBool("FaderBool", true);

                    Cursor.visible = true;
                    StartCoroutine(pauseDelay());
                }
                else if (gamePaused == true)
                {
                    FMODUnity.RuntimeManager.PlayOneShot("event:/Ui/MenuOpen", default);
                    AniGameplayButton.SetBool("FaderBool",false);
                    AniGameplayFader.SetBool("FaderBool", false);

                    gamePaused = false;
                    Time.timeScale = 1;
                    Cursor.visible = false;
                }                
            }
            else if (SceneManager.GetActiveScene().name == "MainMenu" && vidPlaying == false)
            {
                ExitGame();
            }
            else if (SceneManager.GetActiveScene().name == "EndScene")
            {
                LoadCredits();
            }
            else
            {
                LoadMainMenu();
            }
        }
    }

    public void StartIntro()
    {
        buttonsAni = buttons.GetComponent<Animator>();
        buttonsAni.SetTrigger("LoadButtonsOut");
        Animator y = this.GetComponent<Animator>();
        y.SetBool("Start Video", true);

        GameObject x = GameObject.Find("Video Pannel");
        Animator ani = x.GetComponent<Animator>();
        ani.SetTrigger("Start Intro");
        vidPlaying = true;
    }

    public void InGameControls()
    {
        if (AniHowToPlay.GetBool("HowToPlay") == false)
        {
            AniHowToPlay.SetBool("HowToPlay", true);
        }
        else
        {
            AniHowToPlay.SetBool("HowToPlay", false);
        }
    }

    public void LoadGame()
    {
        if(SceneManager.GetActiveScene().name == "EndScene")
        {
            SceneManager.LoadScene("Credits", LoadSceneMode.Single);
        }
        else
        {
            SceneManager.LoadScene("Level_1", LoadSceneMode.Single);
        }
    }

    public void LoadControls()
    {
        SceneManager.LoadScene("HowToPlay", LoadSceneMode.Single);
    }

    public void LoadContributions()
    {
        SceneManager.LoadScene("Settings", LoadSceneMode.Single);
        Time.timeScale = 1;
    }

    public void ExitGame()
    {
        Application.Quit();
    }

    public void ResumeGame()
    {
        AniGameplayButton.SetBool("FaderBool", false);
        AniGameplayFader.SetBool("FaderBool", false);

        gamePaused = false;
        Time.timeScale = 1;

        AniHowToPlay.SetBool("HowToPlay", false);
        Cursor.visible = false;
    }
    
    public void LoadMainMenu()
    {
        SceneManager.LoadScene("MainMenu");
        Time.timeScale = 1;
    }

    IEnumerator pauseDelay()
    {

        yield return new WaitForSeconds(.75f);
        {
            gamePaused = true;
            Time.timeScale = 0;
        }
    }

    public void LoadCredits()
    {
        SceneManager.LoadScene("Credits", LoadSceneMode.Single);
    }
}
