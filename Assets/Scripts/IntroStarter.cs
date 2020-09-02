using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using FMOD.Studio;
using FMODUnity;

public class IntroStarter : MonoBehaviour
{
    Animator ani;
    public float waitTime;
    public Canvas canvas;
    private bool outroAnimaticStarted;
    public static EventInstance outroAnimatic;
     
    // Start is called before the first frame update
    void Start()
    {
        ani = this.GetComponent<Animator>();

        if (SceneManager.GetActiveScene().name == "EndScene")
        {
            StartCoroutine(startVid());
        }
    }

    // Update is called once per frame
    void Update()
    {
        //trigger video if animator says too
        if (ani.GetBool("Start Video") == true)
        {
            PlayVideo();
            ani.SetBool("Start Video", false);
        }
    }

    //start the video
    public void PlayVideo()
    {
        GameObject vid = GameObject.Find("Video Player");
        var x = vid.GetComponent<UnityEngine.Video.VideoPlayer>();        
        x.Play();
        //print("Vid Started" + vid.name);
        waitTime = (float) x.clip.length;
        StartCoroutine(endVid());
    }


    //end video after playing
    IEnumerator endVid()
    {
        print("Change Scene");
        yield return new WaitForSeconds(waitTime);
        canvas.GetComponent<ButtonFunctions>().LoadGame();
    }

    IEnumerator startVid()
    {
        yield return new WaitForSeconds(0.5f);
        PlayVideo();

        outroAnimatic = RuntimeManager.CreateInstance("event:/Music/OutroAnimatic");
        outroAnimatic.start();
        outroAnimatic.release();   
    }

}
