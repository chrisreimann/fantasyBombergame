using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BombBehavior : MonoBehaviour
{   
    // Relevant Gameobjects
    BoxCollider2D bomb_collider;
    public GameObject explosion_alpha;
    List<GameObject> explosion_instances;
    GameObject bombPlacer;
    
    
    // Bomb Properties
    Vector2 pos;
    public bool solid;
    public bool moveable;
    private Vector2 moving;
    public bool exploded = false;
    int explosion_lenght;
    
    

    void Start()
    {
        // Movement through bomb initially possible
        bomb_collider = GetComponent<BoxCollider2D>();
        bomb_collider.enabled = false;
        pos = gameObject.transform.position;
        
        // Initial bomb properties
        moving = new Vector2(0,0);
        solid = false;
        moveable = false;
    }

    void Update()
    {
        // Compute movement
        float movementX = this.gameObject.transform.position.x+moving[0];
        float movementY = this.gameObject.transform.position.y+moving[1];
        
        // Check for obstacles in movement direction
        RaycastHit2D cast = Physics2D.CircleCast(new Vector2(movementX, movementY), 0.01f, new Vector2(0,0));
        if (cast.collider != null && moving!=new Vector2(0,0) && cast.collider.gameObject != this.gameObject)
        {
            this.gameObject.GetComponent<Rigidbody2D>().velocity = new Vector2(0,0);

            if (cast.collider.tag == "Player")
            {
                StopAtRoundedPosition();
            }
            this.gameObject.GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.FreezePosition;
        }

    }

    public void MakeSolid()
    {
        bomb_collider.enabled = true;
        solid = true;
    }

    public void MakeMoveable()
    {
        moveable = true;
    }

    public void Trigger(GameObject player)
    {
        bombPlacer = player;
        StartCoroutine(Fuse());
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (!exploded)
        {
            StartCoroutine(Explode());
            exploded = true;
        }
    }

    IEnumerator Fuse()
    {
        //Waits for 3 seconds before exploding on its own
        yield return new WaitForSeconds(3);
        if (!exploded)
        {
            StartCoroutine(Explode());
            exploded = true;
        }
    }

    IEnumerator Explode()
    {
        
        yield return new WaitForSeconds(0.1f);
        GetComponent<Renderer>().enabled = false;
        GetComponent<Collider2D>().enabled = false;
        List<GameObject> explosion_instances = new List<GameObject>();
        pos = gameObject.transform.position;
        
        // Explosion Middle
        GameObject explosion_middle = Instantiate(explosion_alpha, pos, Quaternion.identity);
        explosion_instances.Add(explosion_middle);
        
        // Explosion for all other directions
        Vector2[] dirList = {new Vector2(0,1), new Vector2(0,-1), new Vector2(1,0), new Vector2(-1,0)};
        foreach (Vector2 dir in dirList)
        {
            for (int i = 1; i < explosion_lenght; i++)
            {
                RaycastHit2D cast = Physics2D.CircleCast(new Vector2(pos.x+dir[0]*i, pos.y+dir[1]*i), 0.3f, new Vector2(0,0));
                if (cast.collider != null && cast.collider.tag == "Destructable")
                {
                    GameObject block = cast.collider.gameObject;
                    block.GetComponent<BlockDestruction>().DestroyBlock();
                    break;
                }

                if (cast.collider != null && cast.collider.tag == "Obstacle")
                {
                    break;
                }

                if (cast.collider != null && cast.collider.tag == "Item")
                {
                    Destroy(cast.collider.gameObject);
                    break;
                }
                
                GameObject exp = Instantiate(explosion_alpha, new Vector2(pos.x+dir[0]*i, pos.y+dir[1]*i), Quaternion.identity);
                explosion_instances.Add(exp);
            }
        }

        // Wait for explosion to finish, then remove all explosion instances
        yield return new WaitForSeconds(0.6f);
        for (int i = 0; i < explosion_instances.Count; i++)
        {
            Destroy(explosion_instances[i]);
        }
        
        // Restore Bomb capacity for bomb placer
        if (bombPlacer)
        {
            bombPlacer.GetComponent<PlaceBomb>().IncMaxBombs();
        }

        Destroy(gameObject);
    }

    public void SetExplosionLength(int playerLength)
    {
        explosion_lenght = playerLength;
    }

    public void Move(Vector2 direction)
    {
        this.gameObject.GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.FreezeRotation;
        this.gameObject.GetComponent<Rigidbody2D>().velocity = new Vector2(direction[0]*6f,direction[1]*6f);
        moving = new Vector2(direction[0]*0.5f,direction[1]*0.5f);
    }

    public void StopAtRoundedPosition()
    {
        int x_rounded = Mathf.RoundToInt(GetComponent<Rigidbody2D>().position.x);
        int y_rounded = Mathf.RoundToInt(GetComponent<Rigidbody2D>().position.y);
        Vector2 pos_rounded = new Vector2(x_rounded, y_rounded);
        this.gameObject.transform.position = pos_rounded;
    }

}
