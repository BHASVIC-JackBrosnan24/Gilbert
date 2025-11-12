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
    private int nextFreePowerUp = 0; //the index of where the next power-up will go

    private float hammerSpeed = 10f; //base speed of hammer
    private int exp = 0; //starting exp (0)
    private int health; //current health of the player
    private int level = 1; //level of the player
    private int nextLevelBarrier = 10;


    void Start()
    {
        health = maxHealth;
        hammerPowerUps = new int[100];
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
        }
    }

    public void addPowerUp(int powerType) //code for adding power-up to power-up list
    {
        hammerPowerUps[nextFreePowerUp] = powerType;
        nextFreePowerUp += 1; //makes sure the next power-up goes into next index
        for (int i = 0; i < hammerPowerUps.Length; i++) { 
            print(hammerPowerUps[i]);
        }
            
    }

}
