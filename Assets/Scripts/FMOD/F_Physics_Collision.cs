using System.Collections;
using UnityEngine;
using FMOD.Studio;
using FMODUnity;
using System.Security.AccessControl;

public class F_Physics_Collision : MonoBehaviour
{

    private bool impact;

    [FMODUnity.EventRef]
    public string eventPath;

    private void OnCollisionEnter2D(Collision2D coll)
    {
        if (coll.gameObject.tag == "Player" && impact == false)
        {

            RuntimeManager.PlayOneShotAttached(eventPath, this.gameObject);
            impact = true;
            StartCoroutine(ImpactReset());

        }

        if (coll.relativeVelocity.magnitude > 3.5)
        {
            RuntimeManager.PlayOneShotAttached(eventPath, this.gameObject);
        }
    }

    IEnumerator ImpactReset()
    {
        yield return new WaitForSeconds(1);
        impact = false;

    }
}
