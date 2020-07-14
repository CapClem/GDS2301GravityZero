using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FMOD;
using FMODUnity;
using FMOD.Studio;

public class F_Player : MonoBehaviour
{
    private CharacterController2D player;
    EventInstance walk;

    private void Start()
    {
        player = GetComponent<CharacterController2D>();
        
    }
    public void Step()
    {
        if (player.m_Grounded == true)
        {
            walk = RuntimeManager.CreateInstance("event:/Player/Footsteps");
            walk.start();
            walk.release();
        }     
    }

    void Jetpack()
    {

    }
}
