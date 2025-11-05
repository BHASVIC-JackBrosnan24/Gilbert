using UnityEngine;

public class PowerUpGemScript : MonoBehaviour
{
    [SerializeField]
    int powerType; //the number assossiated with the power-up

    private void OnCollisionEnter2D(Collision2D collision)//gets called whenever there are collisions
    {
        if (collision.gameObject.CompareTag("Player"))//checks if it collides with the player
        {
            GameObject player = collision.gameObject;
            PlayerStats playerStats = player.GetComponent<PlayerStats>();
            playerStats.addPowerUp(powerType); //adds this power-ups powerType to the array
        }
    }
}
