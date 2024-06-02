using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GameOver : MonoBehaviour
{
    public bool activated;
    
    // Start is called before the first frame update
    void Start()
    {
        activated = false;
        //this.RectTransform.position = new Vector2(150,500);
        this.GetComponent<RectTransform>().anchoredPosition = new Vector2(150,500);
        //print(this.GetComponent<RectTransform>().anchoredPosition);
    }

    // Update is called once per frame
    void Update()
    {
        if(activated)
        {
            this.GetComponent<RectTransform>().anchoredPosition = new Vector2(150,25);
        }
        else
        {
            this.GetComponent<RectTransform>().anchoredPosition = new Vector2(150,1000);
        }
    }

    public void activate(string winner)
    {
        
        //this.gameObject.GetComponent<TextMeshProTextUI>().text = "asd";
        activated = false;
        
        
        print(winner[0..6] + " " + winner[6] + " won the Game!");
    }

}
