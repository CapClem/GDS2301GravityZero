using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LifeCount : MonoBehaviour
{

    public GameObject lifeImage;
    public GameObject livesHolder;
    public List<GameObject> lives;

    int lifeCount;
    public int TotalLife = 3;

    // Start is called before the first frame update
    void Start()
    {
        lifeCount = TotalLife;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Equals))
        {
            AddLife();
            print("You gained a life");
        }
        if (Input.GetKeyDown(KeyCode.Minus))
        {
            LowerLife();
            print("You lost a life");
        }
    }

    public void LowerLife()
    {
        ChangeLifeImages(false);
        lifeCount -= 1;
        //if (lifeCount == 0)
    }

    public void AddLife()
    {
        if (lifeCount < TotalLife)
        lifeCount += 1;
        ChangeLifeImages(true);        
    }

    //update current life images
    public void ChangeLifeImages(bool y)
    {
        if (y == true)
        {
            lives.Add(Instantiate(lifeImage, livesHolder.transform));
            //lives[lives.Length] = lifeImage;
        }
        else if (y == false)
        {
            int x = lives.Count;
            Destroy(lives[x - 1]);
            lives.Remove(lives[x-1]);            
        }
    }
}
