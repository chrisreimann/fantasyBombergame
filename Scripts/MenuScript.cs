using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuScript : MonoBehaviour
{
    GameObject menu;
    bool menuShown;
    bool gameStarted;
    public GameObject centralGrid;

    // Start is called before the first frame update
    void Start()
    {
        menu = GameObject.Find("MenuUI");
        //menu.transform.position = new Vector2(0,3);
        menuShown = false;
        gameStarted = false;
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyUp(KeyCode.Escape))
        {
            if(!menuShown)
            {
                //menu.transform.position = new Vector2(0,3);
                menuShown = true;
            }
            else 
            {
                //menu.transform.position = new Vector2(1000,1000);
                menuShown = false;
            }
            
        }
    }

    public void PlayGame()
    {
        if(gameStarted)
        {
            //menu.transform.position = new Vector2(1000,1000);
        }
        
        else
        {
            centralGrid.GetComponent<MapManagement>().StartGame();
            //menu.transform.position = new Vector2(1000,1000);
            menuShown = false;
            gameStarted = true;
        }
        
    }

    public void ExitGame()
    {
        print("exit was CALLED!");
        if(!menuShown){return;}
        Application.Quit();
    }

}
