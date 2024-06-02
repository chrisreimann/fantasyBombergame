using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour {

    // Components
    Rigidbody2D body;
    public Animator animator;
   
    // Keybindings
    public KeyCode upkey;
    public KeyCode downkey;
    public KeyCode leftkey;
    public KeyCode rightkey;
    
    // Movement Variables
    float horizontal;
    float vertical;
    public float runSpeed = 20.0f;
    Vector2 movement;

    // Forced Movement Booleans
    public bool allowPlayerMovement;
    bool moveDown;
    bool moveLeft;
    bool moveRight;
    bool moveUp;

    // Player Debuffs
    bool stunned;


    void Start ()
    {
        // Initialize Components
        body = GetComponent<Rigidbody2D>(); 

    }

    void Update ()
    {   
        // Vertical Movement
        if (Input.GetKey(upkey) & allowPlayerMovement | moveUp){vertical = 1;}
        if (Input.GetKey(downkey) & allowPlayerMovement | moveDown){vertical = -1;}
        if ((Input.GetKey(upkey) & Input.GetKey(downkey) | !Input.GetKey(upkey) & !Input.GetKey(downkey)) & !moveDown & !moveUp |
            !allowPlayerMovement & !moveDown & !moveUp){vertical = 0;}

        // Horizontal Movement
        if (Input.GetKey(leftkey) & allowPlayerMovement | moveLeft)
        {
            horizontal=-1;
            this.GetComponent<SpriteRenderer>().flipX = true;
        }
        if (Input.GetKey(rightkey) & allowPlayerMovement | moveRight)
        {
            horizontal=1;
            this.GetComponent<SpriteRenderer>().flipX = false;
        }
        if ((Input.GetKey(leftkey) & Input.GetKey(rightkey) | !Input.GetKey(leftkey) & !Input.GetKey(rightkey)) & !moveLeft & !moveRight |
            !allowPlayerMovement & !moveLeft & !moveRight){horizontal=0;}
        movement = new Vector2(horizontal, vertical);

        // Set Animation Parameters
        animator.SetFloat("Horizontal", horizontal);
        animator.SetFloat("Vertical", vertical);
        animator.SetFloat("Speed", movement.sqrMagnitude);

        // Move Player Body
        body.velocity = new Vector2(horizontal * runSpeed, vertical * runSpeed);
    }


    void OnCollisionEnter2D(Collision2D collision)
    {
        // Disable Player Collisions
        if (collision.gameObject.tag == "Player")
        {
            Physics2D.IgnoreCollision(collision.gameObject.GetComponent<Collider2D>(), this.GetComponent<Collider2D>());
        }
    }


    // Move inside Camera for Player Count Display
    public IEnumerator MoveIn(int num)
    {
        moveDown = true;
        yield return new WaitForSeconds(0.8f);
        moveDown = false;

        if (num==1)
        {
            moveLeft = true;
            yield return new WaitForSeconds(0.33f);
            moveLeft = false;
            this.GetComponent<SpriteRenderer>().flipX = false;
        }
        
        if (num==2)
        {
            moveRight = true;
            yield return new WaitForSeconds(0.33f);
            moveRight = false;
            this.GetComponent<SpriteRenderer>().flipX = false;
        }
        
        if (num==3)
        {
            moveLeft = true;
            yield return new WaitForSeconds(0.15f);
            moveLeft = false;
            this.GetComponent<SpriteRenderer>().flipX = false;
        }

        if (num==4)
        {
            moveRight = true;
            yield return new WaitForSeconds(0.15f);
            moveRight = false;
        }
    }


    // Move outside Camera for Player Count Display
    public IEnumerator MoveOut(int num)
    {
        if (num==1)
        {
            moveRight = true;
            yield return new WaitForSeconds(0.33f);
            moveRight = false;
        }

        if (num==2)
        {
            moveLeft = true;
            yield return new WaitForSeconds(0.33f);
            moveLeft= false;
        }
        
        if (num==3)
        {
            moveRight = true;
            yield return new WaitForSeconds(0.15f);
            moveRight = false;
        }

        if (num==4)
        {
            moveLeft = true;
            yield return new WaitForSeconds(0.15f);
            moveLeft = false;
        }

        moveUp = true;
        yield return new WaitForSeconds(0.8f);
        moveUp= false;
    }

    public Vector2 GetMovement()
    {
        return movement;
    }

    // Method activated from hit by Knight Ability
    public IEnumerator StunPlayer()
    {
        print(this.gameObject.name + " got Stunned!");
        allowPlayerMovement = false;
        stunned = true;
        StartCoroutine(BlinkRed());
        

        yield return new WaitForSeconds(2);
        allowPlayerMovement = true;
        stunned = false;
    }

    public IEnumerator BlinkRed()
    {
        while(stunned)
        {
            this.GetComponent<SpriteRenderer>().color = Color.red;
            yield return new WaitForSeconds(0.2f);
            this.GetComponent<SpriteRenderer>().color = Color.white;
            yield return new WaitForSeconds(0.2f);
        }
        
    }

    public IEnumerator MoveInAndOut(int num)
    {
        StartCoroutine(MoveOut(num));
        yield return new WaitForSeconds(1.5f);
        StartCoroutine(MoveIn(num));
    }

}
