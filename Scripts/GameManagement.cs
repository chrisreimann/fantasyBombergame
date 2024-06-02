using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System.Linq;

public class GameManagement : MonoBehaviour
{
    public TMP_Text text;
    public GameObject[] activePlayers;
    
    void Start()
    {
        text.GetComponent<RectTransform>().anchoredPosition = new Vector2(150,1000);
        activePlayers = new GameObject[4];
    }

    public void AddActivePlayer(GameObject player, int index)
    {
        activePlayers[index] = player;
    }

    public void RemoveActivePlayer(GameObject player)
    {
        for(int i = 0; i < activePlayers.Length; i++)
        {
            if(activePlayers[i] == player)
            {
                activePlayers[i] = null;
            }
        }
    }

    public void ClearActivePlayers()
    {
        activePlayers = new GameObject[4];
    }

    public void SetAlphaActivity(bool state)
    {
        GameObject[] playerlist = this.GetComponent<GameSettings>().playerlist;
        foreach(GameObject player in playerlist)
        {
            if(player != null)
            {
                player.SetActive(state);
            }
        }
    }

    public void End(string winner)
    {
        if(winner.Length < 6)
        {
            text.text = "Draw!";
            text.GetComponent<RectTransform>().anchoredPosition = new Vector2(150,25);
        }
        else 
        {
            text.text = winner[0..6] + " " + winner[6] + " wins!";
            print(winner[0..6] + " " + winner[6] + " won the Game!");
            text.GetComponent<RectTransform>().anchoredPosition = new Vector2(150,100);
        }

        StartCoroutine(RestartGame());      
    }

    IEnumerator RestartGame()
    {
        yield return new WaitForSeconds(5f);
        text.GetComponent<RectTransform>().anchoredPosition = new Vector2(150,1000);
        this.GetComponent<MapManagement>().Restart();
    }

    public IEnumerator CheckGameState()
    {
        yield return new WaitForSeconds(0.5f);
        int activePlayerCount = activePlayers.Count(player => player != null);
        
        if (activePlayerCount <= 1 & activePlayerCount > 0)
        {
            foreach(GameObject player in activePlayers)
            {
                if(player == null){continue;}
                MonoBehaviour[] scripts = player.GetComponentsInChildren<MonoBehaviour>();
                foreach(var script in scripts)
                {
                    script.enabled = false;
                }
                End(player.name);
            }
        }
        if(activePlayerCount == 0)
        {
            End("");
        }
    }

    public void Check()
    {
        StartCoroutine(CheckGameState());
    }
}