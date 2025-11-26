using UnityEngine;

public class PUExplosion : MonoBehaviour
{
    GameObject explosion;

    PauseController pauseController;
    SpriteRenderer spriteRenderer;
    void Start()
    {
        explosion = GameObject.FindWithTag("PowerUpController").GetComponent<PowerUpController>().getExplosion();
        spriteRenderer = GetComponent<SpriteRenderer>();
        spriteRenderer.color = Color.black; //colours the hammer black
        pauseController = GameObject.Find("PauseController").GetComponent<PauseController>();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Enemy") && pauseController.unpaused == 1)
        {
            GameObject explode = Instantiate(explosion); //creates the explosion game object
            explode.transform.position = transform.position;
        }
    }
}
