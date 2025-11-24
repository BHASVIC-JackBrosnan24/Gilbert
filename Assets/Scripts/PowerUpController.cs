using UnityEngine;

public class PowerUpController : MonoBehaviour
{
    [SerializeField]
    GameObject powerUp;

    [SerializeField]
    GameObject electricity;

    [SerializeField]
    string[] hammerPowerUps; //array full of the list of hammer power ups, and the probability they trigger

    [SerializeField]
    float[] hammerProb; //the probability of the hammer power-up triggering

    [SerializeField]
    string[] characterPowerUps; //array full of the list of character power ups

    int randP1;
    int randP2;
    int randP3; //the three numbers for the random power-ups

    float randCoordX; //random coordinates
    float randCoordY;

    float timer = 0.2f; //timer until next spawn
    PauseController pauseController;
    private void Start()
    {
        pauseController = GameObject.Find("PauseController").GetComponent<PauseController>();
    }
    void Update()
    {
        timer=timer-(Time.deltaTime * pauseController.unpaused);//decreases timer
        if (timer <= 0) {  //when timer runs out, run spawnPowerUp() and reset timer
            spawnPowerUp();
            timer = 2f;
        }
    }

    void spawnPowerUp() { 
        randP1 = Random.Range(0, hammerPowerUps.Length); //sets randP1 to a random index in HPU
        if (hammerPowerUps.Length > 1) //checks if there is more than one HPU (there always should be)
        {
            randP2 = Random.Range(0, hammerPowerUps.Length);
            while (randP2 == randP1)//makes sure randP2 and randP1 are different
            {
                randP2 = Random.Range(0, hammerPowerUps.Length); //sets randP2 to a random power-up in the array
            }
        }
        else { 
            randP2= Random.Range(0, hammerPowerUps.Length);
        }
        if (hammerPowerUps.Length > 2) //repeats previous process but for randP3
        {
            randP3 = Random.Range(0, hammerPowerUps.Length);
            while (randP3 == randP1 || randP3 == randP2)
            {
                randP3 = Random.Range(0, hammerPowerUps.Length);
            }
        }
        else
        {
            randP3 = Random.Range(0, hammerPowerUps.Length);
        }

        randCoordX = Random.Range(-118.3f, 99.3f); //gets any coordinate on the map
        randCoordY = Random.Range(-66.4f, 79.2f);

        GameObject spawnedGem = Instantiate(powerUp); //instantiates a power-up gem
        Vector2 newPos;
        newPos.x = randCoordX;
        newPos.y = randCoordY;
        spawnedGem.transform.position = newPos; //sets the PUG to the random position
        PowerUpGemScript pugs = spawnedGem.GetComponent<PowerUpGemScript>();
        pugs.setTypes(randP1,randP2,randP3); //sets the gems power-up types
    }

    public string getHammer(int i) {
        return hammerPowerUps[i];
    }

    public string getChar(int i) {
        return characterPowerUps[i];
    }

    public string[] getCharList()
    {
        return characterPowerUps;
    }

    public float getProbability(int i) { //returns the probability the power-up triggers
        return hammerProb[i]; //returns the value in (i,1)
    }

    public GameObject getElec() { 
        return electricity;
    }
}
