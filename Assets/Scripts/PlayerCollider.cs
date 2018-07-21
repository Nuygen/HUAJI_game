using UnityEngine;

/*
 * Provide the obstacles with a way of damaging the player.
 */
public class PlayerCollider : MonoBehaviour
{
  /*
   * A trigger callback to detect when the player's collider has
   * entered the obstacle's. Then simply obtain the PlayerController
   * reference can apply damage. Then remove the obstacle for feedback.
   */
  private void OnTriggerEnter2D(Collider2D other)
  {
        // Obtain a reference to the Player's PlayerController
        if (other.gameObject.tag == "player")
        {
            PlayerController playerController = other.gameObject.GetComponent<PlayerController>();
                // Register damage with player
                //碰撞到的物体是带着不同的标签的.
            playerController.Damage(gameObject.tag);
        }

    // Make this object disappear
    GameObject.Destroy(gameObject);
  }
}
