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

    [SerializeField]
    private GameObject healEffect;

    [SerializeField]
    private GameObject damageEffect;

    private PowerUpController powerUpController;

    private HealthBar healthBar;
    private EXPBar expBar;
    PauseController pauseController;
    SceneChanger sceneChanger;

    private int[] hammerPowerUps; //array of power-ups
    private float[] hammerProb;
    float sum=0;
    private int nextFreeHPowerUp = 0; //the index of where the next power-up will go

    private int[] charPowerUps; //array of power-ups
    private int nextFreeCPowerUp = 0; //the index of where the next power-up will go

    private float hammerSpeed = 10f; //base speed of hammer
    private int exp = 0; //starting exp (0)
    private int health; //current health of the player
    private int level = 1; //level of the player
    private int nextLevelBarrier = 10;
    private int passiveHealing = 0; //value relating to how much the player heals
    private float healTimer = 4;//timer for healing
    private int fireTrail = 0;//value for fire trail


    private int randP1;
    private int randP2;
    private int randP3;


    void Start()
    {
        health = maxHealth;
        hammerPowerUps = new int[500];
        hammerProb = new float[500];
        charPowerUps = new int[500];
        powerUpController = GameObject.FindWithTag("PowerUpController").GetComponent<PowerUpController>();
        healthBar = GameObject.FindWithTag("HealthBar").GetComponent<HealthBar>();
        expBar = GameObject.FindWithTag("EXPBar").GetComponent<EXPBar>();
        healthBar.setHealth(health);
        healthBar.setMax(maxHealth);
        expBar.setEXP(exp);
        expBar.setBoundary(nextLevelBarrier);
        pauseController = GameObject.Find("PauseController").GetComponent<PauseController>();
        sceneChanger = GameObject.Find("SceneChanger").GetComponent<SceneChanger>();
    }

    // Update is called once per frame
    void Update()
    {
        if (pauseController.unpaused == 1)
        {
            if (passiveHealing > 0)
            {
                healTimer -= Time.deltaTime;
                if (healTimer <= 0)
                {
                    health += 2 * passiveHealing; //heals every 4 seconds
                    healTimer = 3;
                    GameObject heal = Instantiate(healEffect); //instantiates a heal effect
                    Vector3 healPos = transform.position;
                    heal.transform.position = healPos;
                }
                if (health > maxHealth)
                {
                    health = maxHealth; //ensures health never ursurps maxHealth
                }
                healthBar.setHealth(health);
            }
        }
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
        expBar.setEXP(exp);
        levelCalc();
    }

    public void damaged(int damaged) {
        health = health - damaged; //reduces player health by damage
        healthBar.setHealth(health);

        GameObject damageSprite = Instantiate(damageEffect);
        damageSprite.transform.position = transform.position;

        if (health <= 0) { //if the player should die from the attack
            Destroy(this.gameObject); //destroys player
            sceneChanger.deathScreen();
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
            expBar.setEXP(exp);
            nextLevelBarrier = 15 * level + nextLevelBarrier/10; //increases exp needed for next level
            expBar.setBoundary(nextLevelBarrier);
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
        hammerProb[nextFreeHPowerUp] = powerUpController.getProbability(powerType);
        sum += hammerProb[nextFreeHPowerUp]; //increases the sum of all probilities
        nextFreeHPowerUp += 1; //makes sure the next power-up goes into next index
    }

    public int getFireTrail() {
        return fireTrail;
    }

    public int[] getHammerPowers() { //getter for hammer power-up list
        return hammerPowerUps;
    }

    public float[] getProb() { //getter for hammer power-up probabilities
        return hammerProb;
    }

    public float getSum() {
        return sum;
    }

    public GameObject getDE(){
        return damageEffect;
    }

    public void heal(int healAmount)
    {
        health += healAmount;
        if (health > maxHealth)
        {
            health = maxHealth;
        }
        GameObject heal = Instantiate(healEffect); //instantiates a heal effect
        Vector3 healPos = transform.position;
        heal.transform.position = healPos;
        healthBar.setHealth(health);
    }

    public void charPowerUp(int powerType) //code for adding character power-up to power-up list, and changing their stats
    {
        charPowerUps[nextFreeCPowerUp] = powerType;
        nextFreeCPowerUp += 1; //makes sure the next power-up goes into next index
        if (powerType == 0) //number assossiated with damage up
        {
            damage += 3;
        }
        else if (powerType == 1) //number assossiated with speed up
        {
            speed += 1;
        }
        else if (powerType == 2) //number assossiated with health up
        {
            health += 25;
            maxHealth += 25;
            healthBar.setMax(maxHealth);
        }
        else if (powerType == 3) //number for fire trail
        {
            fireTrail += 1;
        }
        else if (powerType == 4) //number for passive healing
        {
            passiveHealing += 1;
        }

    }
}
