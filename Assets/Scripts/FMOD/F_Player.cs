using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FMOD;
using FMODUnity;
using FMOD.Studio;

public class F_Player : MonoBehaviour
{
    private CharacterController2D playerCon;

    private PlayerMovement playerMov;
    EventInstance walk;

    private bool jetpackSoundPlayed;

    EventInstance jetPack;

    private void Start()
    {
        playerCon = GetComponent<CharacterController2D>();
        playerMov = GetComponent<PlayerMovement>();

        jetPack = RuntimeManager.CreateInstance("event:/Player/Jetpack");
    }

    private void Update()
    {
        Jetpack();
    }

    public void Step()
    {
        if (playerCon.m_Grounded == true)
        {
            walk = RuntimeManager.CreateInstance("event:/Player/Footsteps");
            walk.start();
            walk.release();
        }     
    }

    void Jetpack()
    {
        if (playerMov.useJetpack == true && jetpackSoundPlayed == false)
        {
            jetPack.start();
            jetpackSoundPlayed = true;
        }
        else if (playerMov.useJetpack == false)
        {
            jetPack.stop(FMOD.Studio.STOP_MODE.ALLOWFADEOUT);
            jetpackSoundPlayed = false;
        }
            
    }
}
