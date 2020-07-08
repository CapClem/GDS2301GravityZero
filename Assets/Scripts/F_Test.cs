using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class F_Test : MonoBehaviour
{

    FMOD.Studio.EventInstance music;
    void Start()
    {
        music = FMODUnity.RuntimeManager.CreateInstance("event:/Music");
        music.start();
    }


}
