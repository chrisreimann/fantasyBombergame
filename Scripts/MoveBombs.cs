using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveBombs : MonoBehaviour
{
    
    public bool hasMover;
    public KeyCode bombStop;
    private GameObject movedBomb;
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
      if (Input.GetKey(bombStop) && movedBomb != null)
      {
        movedBomb.GetComponent<Rigidbody2D>().velocity = new Vector2(0,0);
        movedBomb.GetComponent<BombBehavior>().StopAtRoundedPosition();
        movedBomb.GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.FreezePosition;
      }  
    }

    void OnCollisionEnter2D(Collision2D collision){
        
        
        if (hasMover & collision.collider.tag == "Bomb")
        {
            bool isBombMoveable = collision.collider.gameObject.GetComponent<BombBehavior>().moveable;
            if (isBombMoveable)
            {
                int x_rounded = Mathf.RoundToInt((GetComponent<Rigidbody2D>().position.x - collision.collider.gameObject.transform.position.x)*-1);
                int y_rounded = Mathf.RoundToInt((GetComponent<Rigidbody2D>().position.y - collision.collider.gameObject.transform.position.y)*-1);
                Vector2 pos_rounded = new Vector2(x_rounded, y_rounded);
                collision.collider.GetComponent<BombBehavior>().Move(pos_rounded);
                movedBomb = collision.collider.gameObject;
            }
        }

    }
}
