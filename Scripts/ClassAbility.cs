using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClassAbility : MonoBehaviour
{
    public string className;
    public KeyCode abilityKey;
    bool abilityReady;
    public Animator animator;

    public void Start()
    {
        abilityReady = true;
    }
    
    public void SetClassName(string name)
    {
        className = name;
    }

    // Initiate class specific ability
    public void Update()
    {
        if(abilityReady && Input.GetKeyDown(abilityKey))
        {
            abilityReady = false;
            if(className == "Knight")
            {
                StartCoroutine(CastKnight());
            }
        }
    }

    // Cast Knight Ability with Animation
    IEnumerator CastKnight()
    {
        // Freeze player movement and animate
        animator.SetTrigger("KnightAbility");
        print("starting knight ability animation");
        GetComponent<PlayerMovement>().allowPlayerMovement = false;
        yield return new WaitForSeconds(0.3f);
        KnightAbility();
    }

    public void KnightAbility()
    {
        print("Knight Ability Activated!");
        
        // Check if other player in range
        Vector2 movement = GetComponent<PlayerMovement>().GetMovement();
        float posX = transform.position.x;
        float posY = transform.position.y;
        
        RaycastHit2D[] res = new RaycastHit2D[2];
        int resInt = Physics2D.CircleCast(new Vector2(posX, posY), 1f, new Vector2(0,0), new ContactFilter2D().NoFilter(), results: res );
        
        foreach(RaycastHit2D hit in res)
        {
            if(hit.collider != null && hit.collider.gameObject != this.gameObject && hit.collider.gameObject.tag == "Player")
            {
                StartCoroutine(hit.collider.gameObject.GetComponent<PlayerMovement>().StunPlayer());
            }

        }

        // Freeze player movement
        GetComponent<PlayerMovement>().allowPlayerMovement = false;
        
        // Ability Cooldown
        StartCoroutine(AbilityCooldown());
    }

    IEnumerator AbilityCooldown()
    {
        yield return new WaitForSeconds(0.5f);
        GetComponent<PlayerMovement>().allowPlayerMovement = true;
        yield return new WaitForSeconds(2);
        abilityReady = true;
    }
}
