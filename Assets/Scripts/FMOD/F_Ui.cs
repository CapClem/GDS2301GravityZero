﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FMODUnity;
using FMOD.Studio;

public class F_Ui : MonoBehaviour
{

    EventInstance hover;
    public void OnClick()
    {
        RuntimeManager.PlayOneShot("event:/Ui/ButtonPress", default);
    }
    public void OnclickStart()
    {
        RuntimeManager.PlayOneShot("event:/Ui/ButtonPressStart", default);
    }
    public void OnHover()
    {
        hover = RuntimeManager.CreateInstance("event:/Ui/ButtonHover");
        hover.start();
        hover.release();
    }
    public void OnHover2()
    {
        RuntimeManager.PlayOneShot("event:/Ui/ButtonHover2", default);      
    }
    public void OnHoverExit()
    {
        hover.stop(FMOD.Studio.STOP_MODE.ALLOWFADEOUT);
    }
}
