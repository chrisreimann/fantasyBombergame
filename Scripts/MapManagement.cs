using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using System;

public class MapManagement : MonoBehaviour
{
    // Alpha objects
    public GameObject block_alpha;
    public GameObject item_bomb;
    public GameObject item_fire;
    public GameObject item_mover;
    
    // Control Parameters
    public bool fullMap;
    public bool gameActive;

    public void Start()
    {
        gameActive = false;
    }

    public void StartGame()
    {
        if(gameActive)
        {
            Restart();
        }

        else
        {
            CreateBlocks();
            CreatePlayers();
            GetComponent<GameSettings>().MoveMenusOut();
            gameActive = true;
        }
    }

    public void CreateBlocks()
    {
        if (fullMap)
        {
            List<Vector2> layout = this.GetComponent<MapLayouts>().layout1;
            foreach (Vector2 pos in layout)
            {
                Instantiate(block_alpha, pos, Quaternion.identity);
            }
        }

        else 
        {
            Instantiate(block_alpha, new Vector2(0,0), Quaternion.identity);
            Instantiate(block_alpha, new Vector2(-2,0), Quaternion.identity);
            Instantiate(block_alpha, new Vector2(2,0), Quaternion.identity);
            Instantiate(block_alpha, new Vector2(-4,0), Quaternion.identity);
            Instantiate(block_alpha, new Vector2(4,0), Quaternion.identity);
        }
    }

    public void CreatePlayers()
    {
        // Create Players
        Vector2[] startPositions = {new Vector2(-6,5), new Vector2(6,-5), new Vector2(-6,-5), new Vector2(6,5)};
        GameObject[] playerlist = this.GetComponent<GameSettings>().playerlist;
        string[] classlist = this.GetComponent<GameSettings>().classlist;

        for(int i = 0; i < 4; i++)
        {
            if(playerlist[i]==null){continue;}
            GameObject player = Instantiate(playerlist[i], startPositions[i], Quaternion.identity);
            player.GetComponent<PlayerMovement>().allowPlayerMovement = true;
            player.GetComponent<ClassAbility>().SetClassName(classlist[i]);
            player.GetComponent<AnimationSettings>().SetAnimatorState(Array.IndexOf(this.GetComponent<GameSettings>().availableClasses, classlist[i]));
            this.GetComponent<GameManagement>().AddActivePlayer(player, i);
        }
        
        // Deactivate Alpha Players
        this.GetComponent<GameManagement>().SetAlphaActivity(false);
    }
    
    public void Restart()
    {
        // Reactivate Alpha Players
        this.GetComponent<GameManagement>().SetAlphaActivity(true);

        // Delete Blocks
        GameObject[] blocks = GameObject.FindGameObjectsWithTag("Destructable");
        foreach (GameObject block in blocks)
        {
            if (block != block_alpha){Destroy(block);}
        }
        
        // Delete Items
        GameObject[] items = GameObject.FindGameObjectsWithTag("Item");
        foreach (GameObject item in items)
        {
            if (item != item_fire & item != item_bomb & item != item_mover){Destroy(item);}
        }

        // Delete Players
        GameObject[] activePlayers = this.GetComponent<GameManagement>().activePlayers;
        foreach (GameObject player in activePlayers)
        {
            if(player != null){Destroy(player);}
        }
        
        gameActive = false;
        StartGame();
    }


}
