using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Paralax : MonoBehaviour
{
    //Refference https://www.youtube.com/watch?v=zit45k6CUMk

    private float length, startposX, height, startposY;
    public GameObject camera1;
    public float parallaxEffectX;
    public float parallaxEffectY;

    //If you need to manualy set panning check the box and set panning distance. X value
    public bool ManualRepeat = false;
    public float panning;

    // Start is called before the first frame update
    void Start()
    {
        //set current position of camera
        startposX = transform.position.x;
        startposY = transform.position.y;
        length = GetComponent<SpriteRenderer>().bounds.size.x;
        //print(GetComponent<SpriteRenderer>().bounds.size.x);
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        float temp = (camera1.transform.position.x * (1 - parallaxEffectX)); 
        float dist = (camera1.transform.position.x * parallaxEffectX);
        float tall = (camera1.transform.position.y * parallaxEffectY);

        transform.position = new Vector3(startposX + dist, startposY + tall, transform.position.z);

       /* if (temp > startposX + length) startposX += length;
        else if (temp < startposX - length) startposX -= length;*/

        if (ManualRepeat == false)
        {
            //this will trigger replicate the image once the camera has passed the size of the sprite. I.e. if spite have 1920px spite will duplicate after traveling 19.2 on the x axis
            if (temp > startposX + length) startposX += length;
            else if (temp < startposX - length) startposX -= length;
        }
        else if (ManualRepeat == true)
        {
            if (temp > startposX + panning) startposX += panning;
            else if (temp < startposX - panning) startposX -= panning;
        }


    }
}
