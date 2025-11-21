using UnityEditor;
using UnityEngine;

public class PUElectric : MonoBehaviour
{
    GameObject electricity;

    PauseController pauseController;
    PowerUpController powerUpController;
    SpriteRenderer spriteRenderer;
    void Start()
    {
        powerUpController = GameObject.FindWithTag("PowerUpController").GetComponent<PowerUpController>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        spriteRenderer.color = Color.yellow;
        pauseController = GameObject.Find("PauseController").GetComponent<PauseController>();
        electricity = powerUpController.getElec();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
            if (collision.gameObject.CompareTag("Enemy") && pauseController.unpaused == 1)
            {
                GameObject elecBall = Instantiate(electricity);
                elecBall.transform.position = transform.position;
            }
    }
}
