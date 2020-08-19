using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FMOD.Studio;
using FMODUnity;
using UnityEngine.UI;

public class F_VolumeControl : MonoBehaviour
{
    Bus bus;

    private string busBath;
    void Start()
    {
        bus = RuntimeManager.GetBus("bus/:" + busBath);
    }

    public void SliderVolume(float sliderValue)
    {
        bus.setVolume(sliderValue);
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
