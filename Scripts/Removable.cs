using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class Removable : MonoBehaviour
{

    public Tilemap map;
    public TileBase tile;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        print("removeable triggered");
        Destruct(Vector3Int.FloorToInt(other.transform.position));
    }

    void Destruct(Vector3Int position)
    {
        print("tried destroying" + position);
        map.SetTile(position, tile);
    }

}
