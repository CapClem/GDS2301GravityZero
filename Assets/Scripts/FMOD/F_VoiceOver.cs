using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FMOD.Studio;
using FMODUnity;
public class F_VoiceOver : MonoBehaviour
{

    private bool soundPlayed;

    [FMODUnity.EventRef]
    public string voiceEvent;

    private void OnEnable()
    {
        soundPlayed = false;
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player" && soundPlayed == false)
        {
            Debug.Log("JETPACKFUEL");
            RuntimeManager.PlayOneShotAttached(voiceEvent, this.gameObject);
            soundPlayed = true;
        }
    }
}
