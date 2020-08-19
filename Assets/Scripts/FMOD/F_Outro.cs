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
        outro = RuntimeManager.CreateInstance("event:/Music/OutroAnimatic");
        outro.start();
        outro.release();
    }
}
