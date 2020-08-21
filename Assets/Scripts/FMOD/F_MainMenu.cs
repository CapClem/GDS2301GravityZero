using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FMODUnity;
using FMOD.Studio;
using UnityEditor;

public class F_MainMenu : MonoBehaviour
{
    public EventInstance menuMusic;
    public static EventInstance animaticMusic;
    public bool animaticStarted;
    void Start()
    {
        F_Music.musicStarted = false;
        animaticStarted = false;

        menuMusic = RuntimeManager.CreateInstance("event:/Music/MainMenu");
        menuMusic.start();
        menuMusic.release();    
    }

    public void PlayAnimatic()
    {
        if (animaticStarted == false)
        {
            StartCoroutine(AnimaticMusicStart());
            animaticStarted = true;
        }
    }
    IEnumerator AnimaticMusicStart()
    {
        yield return new WaitForSeconds(1.5f);
        menuMusic.stop(FMOD.Studio.STOP_MODE.ALLOWFADEOUT);
        animaticMusic = RuntimeManager.CreateInstance("event:/Music/Animatic");
        animaticMusic.start();
        animaticMusic.release();

    }
}
