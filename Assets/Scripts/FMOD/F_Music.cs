using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FMOD;
using FMODUnity;
using FMOD.Studio;

public class F_Music : MonoBehaviour
{
    public static EventInstance mainTheme;
    void Start()
    {
        mainTheme = RuntimeManager.CreateInstance("event:/Music/Music (WIP)");
        mainTheme.start();
        mainTheme.release();
    }
}
