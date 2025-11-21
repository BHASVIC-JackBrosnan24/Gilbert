using UnityEngine;

public class ElectricScript : MonoBehaviour
{

    GameObject electricity;

    ParentMeleeEnemy melee;
    ParentRangedEnemy ranged;
    SpriteRenderer spriteRenderer;
    PowerUpController powerUpController;
    private void Start()
    {
        print(6);
        powerUpController = GameObject.FindWithTag("PowerUpController").GetComponent<PowerUpController>();
        electricity = powerUpController.getElec();
        spriteRenderer = GetComponent<SpriteRenderer>();
        spriteRenderer.color = new Color(1, 1, 1, 0.7f); //decreases opacity
        Invoke("destroyThis", 0.2f); //destroys the game object .2 seconds after creation
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
        {
            melee = collision.gameObject.GetComponent<ParentMeleeEnemy>();
            ranged = collision.gameObject.GetComponent<ParentRangedEnemy>();
            print(4);
            if (melee != null)
            {
                melee.damaged(3);
                if (melee.getElectrocuted() == 0) {
                    GameObject elecBall = Instantiate(electricity); //creates more electricity if it hits an unelectrocuted enemy
                    elecBall.transform.position = transform.position;
                }
                melee.electrocute(); //electrocutes the enemy
                
            }
            else if (ranged != null)
            {
                ranged.damaged(3);
                if (ranged.getElectrocuted() == 0)
                {
                    GameObject elecBall = Instantiate(electricity);
                    elecBall.transform.position = transform.position;
                }
                ranged.electrocute();
            }
        }
    }

    private void destroyThis()
    {
        Destroy(gameObject);
    }
}
