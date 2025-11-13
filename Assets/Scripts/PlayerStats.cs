using UnityEngine;

public class PlayerStats : MonoBehaviour
{
    [SerializeField]
    private float speed; //player speed

    [SerializeField]
    private int damage; //hammer damage

    [SerializeField]
    private int maxHealth; //maximum health the player can have

    [SerializeField]
    private GameObject levelUpEffect;

    private int[] hammerPowerUps; //array of power-ups
    private int nextFreeHPowerUp = 0; //the index of where the next power-up will go

    private int[] charPowerUps; //array of power-ups
    private int nextFreeCPowerUp = 0; //the index of where the next power-up will go

    private float hammerSpeed = 10f; //base speed of hammer
    private int exp = 0; //starting exp (0)
    private int health; //current health of the player
    private int level = 1; //level of the player
    private int nextLevelBarrier = 10;

    private int randP1;
    private int randP2;
    private int randP3;


    void Start()
    {
        health = maxHealth;
        hammerPowerUps = new int[100];
        charPowerUps = new int[100];
    }

    // Update is called once per frame
    void Update()
    {

    }

    public float getSpeed() { //returns speed
        return speed;
    }

    public void setSpeed(float aspeed) { //sets speed
        speed = aspeed;
    }

    public int getDamage() //returns damage 
    {
        return damage;
    }

    public void setDamage(int adamage) //sets damage
    {
        damage = adamage;
    }

    public float getHammerSpeed() //returns hammer speed
    {
        return hammerSpeed;
    }
    public void setHammerSpeed(float ahammerSpeed)  //sets hammer speed
    {
        hammerSpeed = ahammerSpeed;
    }

    public int getEXP() //returns exp
    {
        return exp;
    }

    public void setEXP(int EXP) //sets exp
    {
        exp = EXP;
        levelCalc();
    }

    public void damaged(int damaged) {
        health = health - damaged; //reduces player health by damage
        if (health <= 0) { //if the player should die from the attack
            Destroy(this.gameObject); //destroys player
        }
    }

    public int getLevel()
    {
        return level;
    }

    private void levelCalc()
    {
        if (exp >= nextLevelBarrier)
        {
            level += 1; //increases level by 1
            exp = exp - nextLevelBarrier; //resets exp
            nextLevelBarrier = (nextLevelBarrier + 5) * level; //increases exp needed for next level
            GameObject lvlUp = Instantiate(levelUpEffect); //creates the level up effect
            Vector2 lvlUpPos = transform.position;
            lvlUpPos.y = lvlUpPos.y + 1;
            lvlUp.transform.position = lvlUpPos;

            PowerUpController powerUpController = GameObject.FindWithTag("PowerUpController").GetComponent<PowerUpController>();
            string[] characterPowerUps = powerUpController.getCharList();

            randP1 = Random.Range(0, characterPowerUps.Length); //sets randP1 to a random index in HPU
            if (charPowerUps.Length > 1) //checks if there is more than one HPU (there always should be)
            {
                randP2 = Random.Range(0, characterPowerUps.Length);
                while (randP2 == randP1)//makes sure randP2 and randP1 are different
                {
                    randP2 = Random.Range(0, characterPowerUps.Length); //sets randP2 to a random power-up in the array
                }
            }
            else
            {
                randP2 = Random.Range(0, characterPowerUps.Length);
            }
            if (charPowerUps.Length > 2) //repeats previous process but for randP3
            {
                randP3 = Random.Range(0, characterPowerUps.Length);
                while (randP3 == randP1 || randP3 == randP2)
                {
                    randP3 = Random.Range(0, characterPowerUps.Length);
                }
            }
            else
            {
                randP3 = Random.Range(0, characterPowerUps.Length);
            }
            int[] selection = new int[4];
            selection[0] = randP1;
            selection[1] = randP2;
            selection[2] = randP3;
            selection[3] = 1; //makes sure buttonScript knows these are character type
            ButtonController buttonControllerScript = GameObject.FindWithTag("ButtonController").GetComponent<ButtonController>();
            buttonControllerScript.powerUpTime(selection); //starts a power up time with this selection of power-ups
        }
    }

    public void addPowerUp(int powerType) //code for adding power-up to power-up list
    {
        hammerPowerUps[nextFreeHPowerUp] = powerType;
        nextFreeHPowerUp += 1; //makes sure the next power-up goes into next index
    }

    public void charPowerUp(int powerType) //code for adding character power-up to power-up list, and changing their stats
    {
        charPowerUps[nextFreeCPowerUp] = powerType;
        nextFreeCPowerUp += 1; //makes sure the next power-up goes into next index
        if (powerType == 0) //number assossiated with damage up
        {
            damage += 3;
        }

    }
}
