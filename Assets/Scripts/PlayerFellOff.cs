using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerFellOff : MonoBehaviour
{
    // hold player possition for reset
    public Vector3 startPos;
    public Rigidbody2D ridBody;    

    //gravity change variables
    public float lowGravity = 1f;
    public float normGravity = 1.45f;
    public float HighGravity = 2.5f;

    public CharacterController2D playerController;
    public PlayerMovement movementScript;
    public LifeCount LifeCounterScript;

    void Start()
    {
        // set player possition for reset
        startPos = this.gameObject.transform.position;             

        // References character controller script
        playerController = GetComponent<CharacterController2D>();
        movementScript = GetComponent<PlayerMovement>();
        LifeCounterScript = GetComponent<LifeCount>();
    }


    private void OnTriggerEnter2D(Collider2D x)
    {
        //reset player on colission
        if (x.gameObject.tag == "boundry")
        {
           this.transform.position = startPos;
           ridBody.velocity = new Vector3 (0,0,0);

           playerController.m_UpRight = true; //Making sure the player isn't upside down
           playerController.GravityFlip();
            
            LifeCounterScript.ChangeLifeImages(false, 1);
        }        

        //load next scene
        if (x.gameObject.tag == "LevelEndpoint")
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        }

        //Change gravity
        if (x.gameObject.tag == "LowGravity")
        {
            ridBody.gravityScale = lowGravity;
            playerController.m_UpRight = true;
                        
            playerController.GravityFlip();
            print("Gravity Changed");

        }
        else if (x.gameObject.tag == "NormGravity")
        {
            ridBody.gravityScale = normGravity;
            playerController.m_UpRight = true;

            playerController.GravityFlip();
            print("Gravity Changed");

        }
        else if (x.gameObject.tag == "HighGravity")
        {
            ridBody.gravityScale = HighGravity;
            playerController.m_UpRight = true;

            playerController.GravityFlip();
            print("Gravity Changed");
        }

        //Gravity flip collisions to flip the player upside down
        else if (x.gameObject.tag == "LowGravityFlip")
        {
            ridBody.gravityScale = lowGravity * -1;
            playerController.m_UpRight = false;

            playerController.GravityFlip();
            print("Gravity Changed");
        }
        else if (x.gameObject.tag == "NormGravityFlip")
        {
            ridBody.gravityScale = normGravity * -1;
            playerController.m_UpRight = false;

            playerController.GravityFlip();
            print("Gravity Changed");
        }
        else if (x.gameObject.tag == "HighGravityFlip")
        {
            ridBody.gravityScale = HighGravity * -1;
            playerController.m_UpRight = false;
            
            playerController.GravityFlip();
            print("Gravity Changed");
        }
        else if (x.gameObject.tag == "Health")
        {
            LifeCounterScript.ChangeLifeImages(true, 1);
            Destroy(x.gameObject);
        }
        else if (x.gameObject.tag == "FallAble")
        {
            //Trigger Fall
            StartCoroutine(Fall(3, 6, x.gameObject));
        }

        if (x.gameObject.tag == "FuelStation")
        {
            movementScript.regenFuel = true;
        }
    }

    //Trigger fall + reset
    IEnumerator Fall(float fallTimer, float waitTime, GameObject z)
    {
        //save curret position & rotation
        Vector3 SPos = z.transform.position;
        Quaternion rPos = z.transform.rotation;

        Animation ani = z.GetComponent<Animation>();
        ani.Play();
        yield return new WaitForSeconds(fallTimer);
        
        Rigidbody2D y = z.AddComponent<Rigidbody2D>();
        y.constraints = RigidbodyConstraints2D.FreezeRotation;
        ani.Stop();
        yield return new WaitForSeconds(waitTime);

        //Reset
        Destroy(z.GetComponent<Rigidbody2D>());
        z.transform.position = SPos;
        z.transform.rotation = rPos;
    }

    //if we need to dissable jetpack pickup
    private void OnTriggerExit2D(Collider2D x)
    {
        if (x.gameObject.tag == "FuelStation")
        {
            movementScript.regenFuel = false;
            print("no longer refueling");
        }
    }
}
