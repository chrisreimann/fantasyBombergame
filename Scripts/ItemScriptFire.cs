using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemScriptFire : MonoBehaviour
{

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.tag == "Player")
        {
            GameObject player = collision.gameObject;
            player.GetComponent<PlaceBomb>().IncFire();
            Destroy(this.gameObject);
        }
    }
}
