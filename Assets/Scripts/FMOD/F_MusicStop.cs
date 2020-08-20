using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FMOD.Studio;
using FMODUnity;

public class F_MusicStop : MonoBehaviour
{
    public void StopMusic()
    {
        F_Music.mainTheme.stop(FMOD.Studio.STOP_MODE.ALLOWFADEOUT);
    }
}
