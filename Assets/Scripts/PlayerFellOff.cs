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
    public float lowGravity = 3;
    public float normGravity = 1;
    public float HighGravity = 0.5f;

    void Start()
    {
        // set player possition for reset
        startPos = this.gameObject.transform.position;
        //print(startPos);
    }

    // Update is called once per frame
    void Update()
    {
    }

    private void OnTriggerEnter2D(Collider2D x)
    {
        //reset player on colission
        if (x.gameObject.tag == "boundry")
        {
        this.transform.position = startPos;
        print("You fell off the map. Learn to jump better");
        }
        
        //Change gravity
        if (x.gameObject.tag == "LowGravity")
        {
            ridBody.gravityScale = lowGravity;
            print("Gravity Changed");
        }
        else if (x.gameObject.tag == "NormGravity")
        {
            ridBody.gravityScale = normGravity;
            print("Gravity Changed");
        }
        else if (x.gameObject.tag == "HighGravity")
        {
            ridBody.gravityScale = HighGravity;
            print("Gravity Changed");
        }
        
        //load next scene
        if (x.gameObject.tag == "LevelEndpoint")
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        }

    }




}
