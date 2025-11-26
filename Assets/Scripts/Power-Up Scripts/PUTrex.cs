using UnityEngine;

public class PUTrex : MonoBehaviour
{
    private GameObject trex;

    private void Start()
    {
        trex = GameObject.FindWithTag("PowerUpController").GetComponent<PowerUpController>().getTrex(); //gets the trex
        gameObject.GetComponent<SpriteRenderer>().color = Color.green; //makes hammer colour green
    }

    private void OnCollisionEnter(Collision collision)
    {
        GameObject dino = Instantiate(trex);
        dino.transform.position = transform.position; //makes the dino's position equal to the hammer
    }
}
