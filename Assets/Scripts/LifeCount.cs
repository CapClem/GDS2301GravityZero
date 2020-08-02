using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LifeCount : MonoBehaviour
{

    public GameObject lifeImage;
    public GameObject livesHolder;
    public List<GameObject> lives;

    public int TotalLife = 5;
    public int StartingLife = 3;

    public ButtonFunctions changeSceneControler;

    // Start is called before the first frame update
    void Start()
    {
        changeSceneControler = GameObject.Find("Main Camera").GetComponent<ButtonFunctions>();
        ChangeLifeImages(true, StartingLife);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Equals))
        {
            print("You gained a life");
            ChangeLifeImages(true, 1);
        }
        if (Input.GetKeyDown(KeyCode.Minus))
        {
            print("You lost a life");
            ChangeLifeImages(false, 1);
        }
    }

    //Add/Remove health function
    public void ChangeLifeImages(bool y, int u)
    {
        for (int i = 0; i < u; i++)
        {            
            if (y == true && lives.Count + 1 <= TotalLife)
            {
                lives.Add(Instantiate(lifeImage, livesHolder.transform));
                //lives[lives.Length] = lifeImage;
            }
            
            else if (y == false)
            {
                int x = lives.Count;
                Destroy(lives[x - 1]);
                lives.Remove(lives[x - 1]);

                // check if player has died
                if (lives.Count == 0)
                {
                    print("You Lose the game");
                    changeSceneControler.LoadMainMenu();
                }
            }
        }     
    }
}

// scrapped
/*public void LowerLife()
{
    ChangeLifeImages(false, 1);
    lifeCount -= 1;
    //if (lifeCount == 0)
}

public void AddLife()
{
    if (lifeCount < TotalLife)
    lifeCount += 1;
    ChangeLifeImages(true, 1);        
}*/
