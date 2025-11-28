using UnityEngine;

public class PUHypno : MonoBehaviour
{
    PauseController pauseController;
    void Start()
    {
        SpriteRenderer spriteRenderer = GetComponent<SpriteRenderer>();
        spriteRenderer.color = new Color(200, 0, 200); //makes the hammer purple
        pauseController = GameObject.Find("PauseController").GetComponent<PauseController>();
        Hammer hammer = GetComponent<Hammer>();
        hammer.setDamage(0);//makes the hammer do no damage
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Enemy") && pauseController.unpaused == 1)
        {
            collision.gameObject.AddComponent<AllyScript>(); //converts the enemy into an ally
        }
    }
}
