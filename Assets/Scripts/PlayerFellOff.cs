using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerFellOff : MonoBehaviour
{
    // hold player possition for reset
    public Vector3 startPos;
    private Vector3 startScale;
    public Rigidbody2D ridBody;    

    //gravity change variables
    public float lowGravity = 1f;
    public float normGravity = 1.45f;
    public float HighGravity = 2.5f;
    public float startGravity;

    //private bool respawned = false;

    public CharacterController2D playerController;
    public PlayerMovement movementScript;

    void Start()
{
        // set player possition for reset
        startPos = this.gameObject.transform.position;

        // Set player rotation for reset
        //startScale = this.gameObject.transform.localScale;

        startGravity = ridBody.gravityScale;

        // References character controller script
        playerController = GetComponent<CharacterController2D>();
        movementScript = GetComponent<PlayerMovement>();
    }


    private void OnTriggerEnter2D(Collider2D x)
    {
        //reset player on colission
        if (x.gameObject.tag == "boundry")
        {
           this.transform.position = startPos;
           startScale = transform.localScale;
           startScale.y = 1;
           transform.localScale = startScale;
           ridBody.gravityScale = startGravity;
           ridBody.velocity = new Vector3 (0,0,0);
           ridBody.gravityScale = normGravity;

           //playerController.m_UpRight = true;
            print("You fell off the map. Learn to jump better");
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

            if (playerController.m_UpRight != true)
            {
                playerController.GravityFlip();
                print("Gravity Changed");
            }

        }
        else if (x.gameObject.tag == "NormGravity")
        {
            ridBody.gravityScale = normGravity;

            if (playerController.m_UpRight != true)
            {
                playerController.GravityFlip();
                print("Gravity Changed");
            }

        }
        else if (x.gameObject.tag == "HighGravity")
        {
            ridBody.gravityScale = HighGravity;

            if (playerController.m_UpRight != true)
            {
                playerController.GravityFlip();
                print("Gravity Changed");
            }
        }

        //Gravity Flip Player Upside Down
        if (x.gameObject.tag == "LowGravityFlip")
        {
            ridBody.gravityScale = lowGravity * -1;

            if (playerController.m_UpRight != false)
            {
                playerController.GravityFlip();
                print("Gravity Changed");
            }
        }
        else if (x.gameObject.tag == "NormGravityFlip")
        {
            ridBody.gravityScale = normGravity * -1;

            if (playerController.m_UpRight != false)
            {
                playerController.GravityFlip();
                print("Gravity Changed");
            }
        }
        else if (x.gameObject.tag == "HighGravityFlip")
        {
            ridBody.gravityScale = HighGravity * -1;

            if (playerController.m_UpRight != false)
            {
                playerController.GravityFlip();
                print("Gravity Changed");
            }
        }

        else if (x.gameObject.tag == "FuelStation")
        {
            movementScript.regenFuel = true;
        }
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
