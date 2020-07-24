using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FMODUnity;
using FMOD.Studio;

public class F_Player : MonoBehaviour
{
    private CharacterController2D playerCon;

    private PlayerMovement playerMov;
    public EventInstance walk;

    private bool jetpackSoundPlayed;
    private bool rechargeActiv;

    EventInstance jetpackRe;
    EventInstance jetPack;

    private void Start()
    {
        playerCon = GetComponent<CharacterController2D>();
        playerMov = GetComponent<PlayerMovement>();

        jetpackRe = RuntimeManager.CreateInstance("event:/Player/JetpackRefill");
        jetpackRe.set3DAttributes(RuntimeUtils.To3DAttributes(transform));

        jetPack = RuntimeManager.CreateInstance("event:/Player/Jetpack");    
    }

    private void Update()
    {
        Jetpack();
        StepMaterialChange();
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

    void StepMaterialChange()
    {
        if (playerMov.hit.collider != null)
        {
            if (playerMov.hit.collider.tag == "Metal")
            {
                walk.setParameterByName("Material", 1f, false);
            }
            else
                walk.setParameterByName("Material", 0f, false);
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

        if (playerMov.regenFuel == true && playerMov.fuelRemaining > 0 && rechargeActiv == false)
        {
            jetpackRe.start();
            rechargeActiv = true;
        }
        else if (playerMov.regenFuel == false || playerMov.fuelRemaining == 0)
        {
            jetpackRe.stop(FMOD.Studio.STOP_MODE.ALLOWFADEOUT);
            rechargeActiv = false;
        }                    
    }
}
