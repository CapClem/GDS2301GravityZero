using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FMOD.Studio;
using FMODUnity;
using FMOD;

public class F_FallingTile : MonoBehaviour
{

    private bool impact;



    private void OnCollisionEnter2D(Collision2D coll)
    {
        if (coll.gameObject.tag == "Player" && impact == false)
        {
            RuntimeManager.PlayOneShot("event:/Environment/FallingTile", default);
            impact = true;
            StartCoroutine(SoundReset());
        }
    }

    IEnumerator SoundReset()
    {
        yield return new WaitForSeconds(8);
        impact = false;
    }

}
