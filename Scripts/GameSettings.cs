using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System.Linq;
using System;

public class GameSettings : MonoBehaviour
{
    // Player Parameters
    public GameObject player1_alpha;
    public GameObject player2_alpha;
    public GameObject player3_alpha;
    public GameObject player4_alpha;
    public GameObject[] playerlist;
    public string[] classlist;
    public int playerCount;

    // Setting Change Cooldown
    bool decReady;
    public bool[] classReady;

    // Player Class Menu
    public TMP_Text classText1;
    public TMP_Text classText2;
    public TMP_Text classText3;
    public TMP_Text classText4;
    public GameObject playMenu;
    public GameObject InGameMenu;
    public GameObject classMenu1;
    public GameObject classMenu2;
    public GameObject classMenu3;
    public GameObject classMenu4;
    Vector2 posMenu1 = new Vector2(15.5f,5.3f);
    Vector2 posMenu2 = new Vector2(15.5f,0.9f);
    Vector2 posMenu3 = new Vector2(26f,-3.55f);
    Vector2 posMenu4 = new Vector2(26f,-8);
    Vector2 posPlayMenu = new Vector2(-14.5f,1.8f);
    Vector2 posInGameMenu = new Vector2(-26f,1.8f);
    public string[] availableClasses;

    // Keycodes
    public KeyCode escapeKey;

    void Start()
    {
        // Initialize player and class settings
        playerCount = 2; 
        decReady = true;
        classReady = new bool[4] {true, true, true, true};
        playerlist = new GameObject[4] {player1_alpha, player2_alpha, null, null};
        classlist = new string[4] {"Knight", "Knight", "Knight", "Knight"};
    }

    void Update()
    {
        // Update class text
        classText1.text = classlist[0];
        classText2.text = classlist[1];
        classText3.text = classlist[2];
        classText4.text = classlist[3];

        // Update menu positions
        classMenu1.GetComponent<RectTransform>().localPosition = Vector2.MoveTowards(classMenu1.GetComponent<RectTransform>().localPosition, posMenu1, 0.1f);
        classMenu2.GetComponent<RectTransform>().localPosition = Vector2.MoveTowards(classMenu2.GetComponent<RectTransform>().localPosition, posMenu2, 0.1f);
        classMenu3.GetComponent<RectTransform>().localPosition = Vector2.MoveTowards(classMenu3.GetComponent<RectTransform>().localPosition, posMenu3, 0.1f);
        classMenu4.GetComponent<RectTransform>().localPosition = Vector2.MoveTowards(classMenu4.GetComponent<RectTransform>().localPosition, posMenu4, 0.1f);
        playMenu.GetComponent<RectTransform>().localPosition = Vector2.MoveTowards(playMenu.GetComponent<RectTransform>().localPosition, posPlayMenu, 0.1f);
        InGameMenu.GetComponent<RectTransform>().localPosition = Vector2.MoveTowards(InGameMenu.GetComponent<RectTransform>().localPosition, posInGameMenu, 0.1f);

        // Check for escape key
        if(Input.GetKeyDown(escapeKey) && GetComponent<MapManagement>().gameActive)
        {
            posInGameMenu = new Vector2(-14.5f,1.8f);
        }
    }

    public void ChangeClassRight(int playerNum)
    {
        if(classReady[playerNum])
        {
            classlist[playerNum] = availableClasses[(Array.IndexOf(availableClasses, classlist[playerNum]) + 1) % 3];
            StartCoroutine(playerlist[playerNum].GetComponent<PlayerMovement>().MoveInAndOut(playerNum+1));
            StartCoroutine(playerlist[playerNum].GetComponent<AnimationSettings>().ChangeAnimatorState(Array.IndexOf(availableClasses, classlist[playerNum])));
            StartCoroutine(ClassCooldown(playerNum));
        }
        
    }

    public void ChangeClassLeft(int playerNum)
    {
        if(classReady[playerNum])
        {
            classlist[playerNum] = availableClasses[(Array.IndexOf(availableClasses, classlist[playerNum]) + 2) % 3];
            StartCoroutine(playerlist[playerNum].GetComponent<PlayerMovement>().MoveInAndOut(playerNum+1));
            StartCoroutine(playerlist[playerNum].GetComponent<AnimationSettings>().ChangeAnimatorState(Array.IndexOf(availableClasses, classlist[playerNum])));
            StartCoroutine(ClassCooldown(playerNum));
        }
        
    }

    IEnumerator ClassCooldown(int playerNum)
    {
        classReady[playerNum] = false;
        yield return new WaitForSeconds(2.5f);
        classReady[playerNum] = true;
    }

     public void IncPlayer()
    {
        if(playerCount == 4){return;}
        playerCount++;

        if(playerCount == 3)
        {
            MovePlayerIn(player3_alpha, 3);
            playerlist[2] = player3_alpha;
            posMenu3 = new Vector2(15.5f,-3.55f);
        }
        
        if(playerCount == 4)
        {
            MovePlayerIn(player4_alpha, 4);
            playerlist[3] = player4_alpha;
            posMenu4 = new Vector2(15.5f,-8f);
        }
    }

    public void DecPlayer()
    {
        
        if(playerCount == 2 | !decReady){return;}
        playerCount--;
        if(playerCount == 2)
        {
            MovePlayerOut(player3_alpha, 3);
            playerlist[2] = null;
            posMenu3 = new Vector2(26f, -3.55f);
        }
        
        if(playerCount == 3)
        {
            MovePlayerOut(player4_alpha, 4);
            playerlist[3] = null;
            posMenu4 = new Vector2(26f, -8);
        }
    }

    public void MovePlayerIn(GameObject player, int num)
    {
        StartCoroutine(player.GetComponent<PlayerMovement>().MoveIn(num));
        StartCoroutine(DecCooldown());
    }

    public void MovePlayerOut(GameObject player, int num)
    {
        StartCoroutine(player.GetComponent<PlayerMovement>().MoveOut(num));
    }

    IEnumerator DecCooldown()
    {
        decReady = false;
        yield return new WaitForSeconds(1f);
        decReady = true;
    }

    public void MoveMenusOut()
    {
        posMenu1 = new Vector2(26f,5.3f);
        posMenu2 = new Vector2(26f,0.9f);
        posMenu3 = new Vector2(26f,-3.55f);
        posMenu4 = new Vector2(26f,-8f);
        posPlayMenu = new Vector2(-26f, 1.8f);
    }

    public void InGameMenuOut()
    {
        posInGameMenu = new Vector2(-26f,1.8f);
    }

}
