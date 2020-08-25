using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class F_MusicFade : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            F_Music.mainTheme.stop(FMOD.Studio.STOP_MODE.ALLOWFADEOUT);
        }
    }
}
