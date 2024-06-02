using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDeath : MonoBehaviour
{
   public GameObject deathAnimation_alpha;
   public GameObject grid;

   public void OnTriggerEnter2D()
   {
      print("Player is dead!");
      StartCoroutine(Death());
   }

   IEnumerator Death()
   {
      // Disable Player Sprite
      this.GetComponent<SpriteRenderer>().enabled = false;
      yield return new WaitForSeconds(0.3f);
      
      // Create Death Animation
      int x_rounded = Mathf.RoundToInt(this.gameObject.transform.position.x);
      int y_rounded = Mathf.RoundToInt(this.gameObject.transform.position.y);
      Vector2 pos_rounded = new Vector2(x_rounded, y_rounded);
      GameObject deathAnimation = Instantiate(deathAnimation_alpha, pos_rounded, Quaternion.identity);

      // Resolve Death Animation
      yield return new WaitForSeconds(1.3f);
      Destroy(deathAnimation);

      // Remove Player from activePlayers array
      GameManagement game = grid.GetComponent<GameManagement>();
      game.RemoveActivePlayer(this.gameObject);
      game.Check();

      // Destroy Player Object
      Destroy(this.gameObject);
   }

}
