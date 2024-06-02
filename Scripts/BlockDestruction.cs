using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class BlockDestruction : MonoBehaviour
{
    // Item Probabilities
    public int chanceBomb;
    public int chanceFire;
    public int chanceMover;

    // Item List
    private List<string> itemList;

    public void Start()
    {
        // Create Item List with given probabilities
        itemList = new List<string>();
        itemList.AddRange(Enumerable.Repeat("Item_Bomb", chanceBomb));
        itemList.AddRange(Enumerable.Repeat("Item_Fire", chanceFire));
        itemList.AddRange(Enumerable.Repeat("Item_Mover", chanceMover));
        itemList.AddRange(Enumerable.Repeat("Nothing", 100-chanceBomb-chanceFire-chanceMover));

    }

    public void DestroyBlock()
    {
        StartCoroutine(Selfdestruct());
    }

    IEnumerator Selfdestruct()
    {
        this.GetComponent<Animator>().SetBool("Destruct", true);
        yield return new WaitForSeconds(0.2f);
        SpawnItem();
        Destroy(this.gameObject);
    }

    public void SpawnItem()
    {
        // Select random item
        System.Random rnd = new System.Random();
        int rndNum = rnd.Next(itemList.Count);

        if (itemList[rndNum] == "Nothing"){return;}
        
        // Instantiate item
        GameObject item = GameObject.Find(itemList[rndNum]);
        Vector2 itemPos = new Vector2(this.transform.position.x + 0.05f, this.transform.position.y - 0.05f);
        Instantiate(item, itemPos, Quaternion.identity);
    }

}
