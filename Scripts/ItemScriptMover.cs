using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemScriptMover : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.tag == "Player")
        {
            GameObject player = collision.gameObject;
            player.GetComponent<MoveBombs>().hasMover = true;
            Destroy(this.gameObject);
        }
    }
}
