using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FMOD.Studio;
using FMODUnity;

public class F_FallingTile : MonoBehaviour
{

    private bool impact;

    private void OnCollisionEnter2D(Collision2D coll)
    {
        if (coll.gameObject.tag == "Player" && impact == false)
        {
            RuntimeManager.PlayOneShotAttached("event:/Environment/FallingTile", this.gameObject);
            impact = true;
        }
    }
}
