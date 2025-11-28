using UnityEngine;

public class PUTrex : MonoBehaviour
{
    private GameObject trex;
    PauseController pauseController;
    PowerUpController powerUpController;

    private void Start()
    {
        powerUpController = GameObject.FindWithTag("PowerUpController").GetComponent<PowerUpController>(); //gets the trex
        gameObject.GetComponent<SpriteRenderer>().color = Color.green; //makes hammer colour green
        pauseController = GameObject.Find("PauseController").GetComponent<PauseController>();
        trex = powerUpController.getTrex();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Enemy") && pauseController.unpaused == 1)
        {
            print(1);
            GameObject dino = Instantiate(trex);
            dino.transform.position = transform.position; //makes the dino's position equal to the hammer
        }
    }
}
