using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FMOD.Studio;
using FMODUnity;

public class F_Outro : MonoBehaviour
{
    EventInstance outro;
    void Start()
    {
        F_Music.mainTheme.stop(FMOD.Studio.STOP_MODE.ALLOWFADEOUT);
        F_Music.mainTheme.stop(FMOD.Studio.STOP_MODE.ALLOWFADEOUT);
        outro = RuntimeManager.CreateInstance("event:/Music/OutroAnimatic");
        outro.start();
        outro.release();
    }
}
