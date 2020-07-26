using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IntroStarter : MonoBehaviour
{
    Animator ani;
        

    // Start is called before the first frame update
    void Start()
    {
        ani = this.GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if (ani.GetBool("Start Video") == true)
        {
            PlayVideo();
            ani.SetBool("Start Video", false);
        }
    }

    public void PlayVideo()
    {
        GameObject vid = GameObject.Find("Video Player");
        var VideoPlayer = vid.AddComponent<UnityEngine.Video.VideoPlayer>();
        VideoPlayer.Play();
        print("Vid Started");
    }
}
