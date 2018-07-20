using UnityEngine;
using UnityEngine.UI;

/*
 * On screen HUD to display current health.
 */
public class PlayerHud : MonoBehaviour
{
    public Text life_text;
    private PlayerController playerController;
    //private Texture2D halfHeart;
    private Texture2D heart;

    /*
     * Load and store the heart images and cache the PlayerController
     * component for later.
     */
    private void Start()
    {
        playerController = GetComponent<PlayerController>();
        heart = Resources.Load<Texture2D>("heart");
       // halfHeart = Resources.Load<Texture2D>("halfHeart");
    }

    /*
     * Using the current health from the PlayerController, display the
     * correct number of hearts and half hearts.
     */
    private void OnGUI()
    {
        GUI.DrawTexture(new Rect(10, 10, 50, 50), heart);
        life_text.text = " × "+playerController.GetHealth().ToString();
    }
}
