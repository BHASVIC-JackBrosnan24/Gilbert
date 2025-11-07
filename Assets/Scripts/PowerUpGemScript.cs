using UnityEngine;

public class PowerUpGemScript : MonoBehaviour
{
    int[] powerType=new int[3]; //the number assossiated with the power-up
    private void Start()
    {
    }
    private void OnCollisionEnter2D(Collision2D collision)//gets called whenever there are collisions
    {
        if (collision.gameObject.CompareTag("Player"))//checks if it collides with the player
        {
            GameObject player = collision.gameObject;
            PlayerStats playerStats = player.GetComponent<PlayerStats>();
            //playerStats.addPowerUp(powerType); //adds this power-ups powerType to the array
            for (int i = 0; i < 3; i++)
            {
                print(powerType[i]);
            }
            Destroy(this.gameObject);
        }
    }

    public void setTypes(int p1, int p2, int p3) { //setter for the power type array
        powerType[0]=p1;
        powerType[1]=p2;
        powerType[2]=p3;
    }

    public int[] getTypes() { //getter for the power type array
        return powerType;
    }
}
