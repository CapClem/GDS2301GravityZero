using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FMOD;
using FMODUnity;
using FMOD.Studio;
using System.Diagnostics.Contracts;

public class F_Music : MonoBehaviour
{
    public static EventInstance mainTheme;

    public static bool musicStarted;

  

    [SerializeField]
    private float parameterValue;
    void Start()
    {

        if (musicStarted == false)
        {
            mainTheme = RuntimeManager.CreateInstance("event:/Music/MusicDynamic");
            mainTheme.start();
            mainTheme.release();
            musicStarted = true;
        }   
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            mainTheme.setParameterByName("VerseChange", parameterValue, false);
        }

    }
}
