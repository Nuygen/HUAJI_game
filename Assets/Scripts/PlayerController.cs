using UnityEngine;
using UnityEngine.SceneManagement;

/*
 * Behaviour to handle keyboard input and also store the player's
 * current health.
 */
public class PlayerController : MonoBehaviour
{
    public GameObject shotPrefabs;
    private Rigidbody2D rigidbody2d;
    private float health;
    private int canJump;

   /*
   * Apply initial health and also store the Rigidbody2D reference for
   * future because GetComponent<T> is relatively expensive.
   */
    private void Start()
    {
        health = 6;
        canJump = 2;
        rigidbody2d = GetComponent<Rigidbody2D>();
    }

    /*
    * Remove one health unit from player and if health becomes 0, change
    * scene to the end game scene.
    */
    public void Damage(string tag)
    {
        if (tag == "bomb")
        {
            health -= 1;
        }
        else if (tag == "bottle")
        {
            health -= 0.5f;
        }
        else if (tag == "half_life")
        {
            health += 0.5f;
        }
        else if (tag == "whole_life")
        {
            health += 1;
        }


        if(health <= 0)
        {
                SceneManager.LoadScene("EndGame");
        }
    }

    /*
    * Accessor for health variable, used by he HUD to display health.
    */
    public float GetHealth()
    {
        return health;
    }

    /*
    * Poll keyboard for when the up arrow is pressed. If the player can jump
    * (is on the ground) then add force to the cached Rigidbody2D component.
    * Finally unset the canJump flag so the player has to wait to land before
    * another jump can be triggered.
    */
    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.UpArrow))
        {
            if(canJump > 0)
            {
                //rigidbody2d.velocity.Set(0, 0);是不行的.
                //增加的跳跃的效果更加符合我们玩家的理解正常的二段跳跃
                rigidbody2d.velocity = new Vector2(0, 0);
                rigidbody2d.AddForce(new Vector2(0, 1200));
                canJump -= 1;
            }
        }
        //滑稽表情的旋转
        transform.Rotate(new Vector3(0, 0, -360 * Time.deltaTime));

        if(Input.GetKey(KeyCode.Space))
        {
            //调整scale大小
            if(transform.localScale.x < 3)
                transform.localScale += new Vector3(0.1f, 0.1f, 0);
        }
        if (Input.GetKey(KeyCode.V))
        {
            if(transform.localScale.x > 0.3)
                transform.localScale -= new Vector3(0.1f, 0.1f, 0);
        }

        if(Input.GetKeyDown(KeyCode.F))
        {
            Vector2 shot = gameObject.transform.position;
            shot.x = shot.x +  gameObject.transform.localScale.x + 1;
            Instantiate(shotPrefabs, shot, Quaternion.identity);
        }
    }

    /*
    * If the player has collided with the ground, set the canJump flag so that
    * the player can trigger another jump.
    */
    private void OnCollisionEnter2D(Collision2D other)
    {
        if( other.gameObject.tag == "Ground")
        {
            canJump = 2;
        }
    }

}
