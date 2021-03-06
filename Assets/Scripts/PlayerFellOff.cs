﻿using System.Collections;
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
    public Animator gravityAni;

    public CharacterController2D playerController;
    public PlayerMovement movementScript;
    public LifeCount LifeCounterScript;   

    Animator SaveAni;

    bool resetPlayer = false;
    public float ResetTimer = 0.5f;

    public GameObject fader;
    Animator fadeAni;

    public GameObject fader2;
    Animator fadeAni2;

    void Start()
    {
        fadeAni = fader.GetComponent<Animator>();
        fadeAni2 = fader2.GetComponent<Animator>();
        // set player possition for reset
        startPos = this.gameObject.transform.position;             

        // References character controller script
        playerController = GetComponent<CharacterController2D>();
        movementScript = GetComponent<PlayerMovement>();
        LifeCounterScript = GetComponent<LifeCount>();
        SaveAni = GameObject.Find("SaveNewResetPos").GetComponent<Animator>();
    }


    private void OnTriggerEnter2D(Collider2D x)
    {
        //reset player on colission
        if (x.gameObject.tag == "boundry")
        {
            StartCoroutine(ResetPlayer(ResetTimer));
        }   
        else if (x.gameObject.tag == "Spikes")
        {
            StartCoroutine(ResetPlayer(ResetTimer));
        }

        //load next scene
            if (x.gameObject.tag == "LevelEndpoint")
        {
            //if (SceneManager.GetActiveScene().name == "Level_3")
            {
                //SceneManager.LoadScene("Credits", LoadSceneMode.Single);
            }
            //else
            {
                StartCoroutine(fadeout());
            }
        }

        //Change gravity
        if (x.gameObject.tag == "LowGravity")
        {
            SetGravityAniParameters(true, false, false);

            ChangeGravityScale(lowGravity, 1);
            playerController.m_UpRight = true;
                        
            playerController.GravityFlip();
            print("Gravity Changed");

        }
        else if (x.gameObject.tag == "NormGravity")
        {
            SetGravityAniParameters(false, true, false);

            ChangeGravityScale(normGravity, 1);
            playerController.m_UpRight = true;

            playerController.GravityFlip();
            print("Gravity Changed");

        }
        else if (x.gameObject.tag == "HighGravity")
        {
            SetGravityAniParameters(false, false, true);

            ChangeGravityScale(HighGravity, 1);
            playerController.m_UpRight = true;

            playerController.GravityFlip();
            print("Gravity Changed");
        }

        //Gravity flip collisions to flip the player upside down
        else if (x.gameObject.tag == "LowGravityFlip")
        {
            SetGravityAniParameters(true, false, false);

            ChangeGravityScale(lowGravity, -1);
            playerController.m_UpRight = false;

            playerController.GravityFlip();
            print("Gravity Changed");
        }
        else if (x.gameObject.tag == "NormGravityFlip")
        {
            SetGravityAniParameters(false, true, false);

            ChangeGravityScale(normGravity, -1);
            playerController.m_UpRight = false;

            playerController.GravityFlip();
            print("Gravity Changed");
        }
        else if (x.gameObject.tag == "HighGravityFlip")
        {
            SetGravityAniParameters(false, false, true);

            ChangeGravityScale(HighGravity, -1);
            playerController.m_UpRight = false;
            
            playerController.GravityFlip();
            print("Gravity Changed");
        }

        if (x.gameObject.tag == "Health")
        {
            LifeCounterScript.ChangeLifeImages(true, 1);
            Destroy(x.gameObject);
        }
        
        else if (x.gameObject.tag == "FallAble")
        {
            //Trigger Fall
            StartCoroutine(Fall(3, 6, x.gameObject));
        }
        
        else if(x.gameObject.tag == "Bridge")
        {
            x.GetComponent<Animator>().SetTrigger("PlayerCollided");
            Rigidbody2D y = x.gameObject.AddComponent<Rigidbody2D>();
            if (y != null)
            {
                y.gravityScale = 3;
            }
        }

        //Reset position
        if (x.gameObject.tag == "Save Point")
        {
            startPos = x.gameObject.transform.position;
            SaveAni.SetTrigger("SaveGame");
            Destroy(x);
        }

        if (x.gameObject.tag == "FuelStation")
        {
            movementScript.regenFuel = true;
        }
    }
    
    private void OnTriggerExit2D(Collider2D x)
    {
        //To disable jetpack fuel regeneration
        if (x.gameObject.tag == "FuelStation")
        {
            movementScript.regenFuel = false;
            print("no longer refueling");
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
        if (playerController.m_UpRight != true)
        {
            y.gravityScale = -1;
        }
        y.constraints = RigidbodyConstraints2D.FreezeRotation;
        ani.Stop();
        z.GetComponent<BreakPlatform>().iFell = true;

        yield return new WaitForSeconds(waitTime);

        //Reset
        z.GetComponent<BreakPlatform>().iFell = false;
        Destroy(z.GetComponent<Rigidbody2D>());

        z.transform.position = SPos;
        z.transform.rotation = rPos;
    }
    
    //trigger player reset
    IEnumerator ResetPlayer(float x)
    {
        if (resetPlayer == false)
        {
            //trigger fade out
            fadeAni.SetTrigger("Start");
            resetPlayer = true;

            yield return new WaitForSeconds(x);
            this.transform.position = startPos;
            ridBody.velocity = new Vector3(0, 0, 0);
            playerController.m_UpRight = true; //Making sure the player isn't upside down
            playerController.GravityFlip();

            //reset Gravity
            this.ridBody.gravityScale = normGravity;
            LifeCounterScript.ChangeLifeImages(false, 1);

            resetPlayer = false;
        }
        
    }

    IEnumerator fadeout() 
    {
        fadeAni2.SetTrigger("FadeOut");
        yield return new WaitForSeconds(1.5f);
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }    

    //Conrtols gravity changes wilst using the jetpack 
    void ChangeGravityScale(float gravScale, float posOrNeg)
    {
        movementScript.startGravity = gravScale * posOrNeg;

        if (movementScript.useJetpack == true)
        {
            ridBody.gravityScale = normGravity * posOrNeg;
        }
        else
        {
            ridBody.gravityScale = movementScript.startGravity;
        }        
    }

    void SetGravityAniParameters(bool low, bool norm, bool high)
    {

        gravityAni.SetBool("LowGravity", low);
        gravityAni.SetBool("NormGravity", norm);
        gravityAni.SetBool("HighGravity", high);
    }
}
