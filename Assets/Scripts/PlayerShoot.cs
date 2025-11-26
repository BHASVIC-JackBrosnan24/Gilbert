using Unity.VisualScripting;
using UnityEngine;

public class PlayerShoot : MonoBehaviour
{
    [SerializeField]
    private GameObject Hammer;

    void Update()
    {
        if (Input.GetMouseButtonDown(0) == true) {
            GameObject hammer = Instantiate(Hammer);
            Vector2 hammerPos = transform.position;
            hammer.transform.position = hammerPos;
            
            PlayerStats playerStats = GameObject.FindWithTag("Player").GetComponent<PlayerStats>();// gets the players stats
            float[] probabilities = playerStats.getProb();
            int[] powerUps = playerStats.getHammerPowers();
            float sum = playerStats.getSum(); //gets all relevant values

            if (sum > 0)
            {
                int powerType = -1;
                float random;

                if (sum > 100)
                {
                    random = Random.Range(0, sum); //generates a random value between 0 and sum
                }
                else
                {
                    random = Random.Range(0, 100); //generates a random value between 0 and 100
                }
                for (int i = 0; i < probabilities.Length; i++)
                {
                    random -= probabilities[i]; //decreases the random value by the probability power-up i triggers
                    if (random <= 0) //if that probability makes random < 0 
                    {
                        powerType = powerUps[i]; //chooses that power-up to be used on the hammer
                        break; //stops the loop
                    }
                }
                if (powerType != -1)
                {
                    switch (powerType)
                    {
                        case 0:
                            hammer.AddComponent<PUFire>();
                            break;
                        case 1:
                            hammer.AddComponent<PUElectric>();
                            break;
                        case 2:
                            hammer.AddComponent<PUExplosion>();
                            break;
                    }
                }
            }
        }
    }
}
