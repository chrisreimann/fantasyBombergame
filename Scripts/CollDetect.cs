using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollDetect : MonoBehaviour
{
    public bool block_collision = false;
    
    void OnCollisionEnter2D(Collision2D collision)
    {
        print(collision.collider.name);
    }


}
