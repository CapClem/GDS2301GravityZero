using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BreakPlatform : MonoBehaviour
{
    public bool iFell = false;
    private bool colliedWithSomething = false;

    private void Update()
    {
        if(iFell == true)
        {
            if(colliedWithSomething == true)
            {
                gameObject.GetComponent<SpriteRenderer>().enabled = false;
                gameObject.GetComponent<BoxCollider2D>().enabled = false;
                colliedWithSomething = false;
            }            
        }
        else if (colliedWithSomething == false)
        {
            gameObject.GetComponent<SpriteRenderer>().enabled = true;
            gameObject.GetComponent<BoxCollider2D>().enabled = true;
        }
        else
        {                        
            gameObject.GetComponent<SpriteRenderer>().enabled = true;
            gameObject.GetComponent<BoxCollider2D>().enabled = true;
        }
    }

    private void OnTriggerEnter2D(Collider2D x)
    {

        if (x.gameObject.tag == "Spikes")
        {
            colliedWithSomething = true;
        }
        if (x.gameObject.layer == LayerMask.NameToLayer("Platforms"))
        {
            colliedWithSomething = true;
        }
    }


}
