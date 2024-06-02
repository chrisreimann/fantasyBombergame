using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlaceBomb : MonoBehaviour
{
    GameObject bomb_placed;
    public GameObject bomb_alpha;
    
    // Player specific bomb-parameters
    private int maxBombs;
    private int expLength;
    public bool bombReady = true; 
    public KeyCode bombkey;


    void Start()
    {
        // Initial player specific bomb-parameters
        maxBombs = 1;
        expLength = 2;
    }

    private void FixedUpdate()
    {  
        // Place a bomb
        if (Input.GetKey(bombkey) & bombReady & maxBombs > 0)
        {
            // Find placing position for bomb
            int x_rounded = Mathf.RoundToInt(GetComponent<Rigidbody2D>().position.x);
            int y_rounded = Mathf.RoundToInt(GetComponent<Rigidbody2D>().position.y);
            Vector2 pos_rounded = new Vector2(x_rounded, y_rounded+0.2f);

            // Instantiate and trigger bomb
            bomb_placed = Instantiate(bomb_alpha, pos_rounded, Quaternion.identity);
            bomb_placed.GetComponent<BombBehavior>().SetExplosionLength(expLength);
            bomb_placed.GetComponent<BombBehavior>().Trigger(this.gameObject);
            
            // Dec bomb parameters
            bombReady = false;
            maxBombs = maxBombs - 1;
        }

        // Make Bomb solid if far away
        if (!bombReady && bomb_placed != null)
        {
            Vector3 bombPosition = bomb_placed.transform.position;
            Vector3 playerPosition = gameObject.transform.position;
            float distanceToBomb = Vector3.Distance(playerPosition, bombPosition);

            if (distanceToBomb > 0.8f)
            {
                bomb_placed.GetComponent<BombBehavior>().MakeSolid();
                bombReady = true;
                StartCoroutine(MakeMoveable());
            }
        }
        
            
    }

    IEnumerator MakeMoveable()
    {
        // Wait short duration so that bomb is not directly moved after becoming solid
        yield return new WaitForSeconds(0.1f);
        bomb_placed.GetComponent<BombBehavior>().MakeMoveable();
    }

    public void IncMaxBombs()
    {
        maxBombs = maxBombs + 1;
    }

    public void IncFire()
    {
        expLength = expLength + 1;
    }
}
