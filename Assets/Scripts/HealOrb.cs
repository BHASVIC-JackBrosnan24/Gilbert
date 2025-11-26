using UnityEngine;

public class HealOrb : MonoBehaviour
{

    private void Start()
    {
        Invoke("destroyMethod", 8);
    }

    private void destroyMethod() { 
        Destroy(gameObject);
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            PlayerStats playerStats = collision.gameObject.GetComponent<PlayerStats>();
            playerStats.heal(1);
            Destroy(gameObject);
        }
    }
}