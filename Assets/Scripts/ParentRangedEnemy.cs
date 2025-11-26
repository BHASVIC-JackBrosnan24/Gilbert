using UnityEngine;
using UnityEngine.UIElements;

public class ParentRangedEnemy : MonoBehaviour
{
    [SerializeField]
    private int health; //how much damage the enemy can take
    [SerializeField]
    private int damage; //how much damage the enemy deals to the player
    [SerializeField]
    private float speed; //how fast the enemy will move
    [SerializeField]
    private float attackRate; //how fast the enemy will attack
    [SerializeField]
    private int expYield;//how much exp the enemy gives
    [SerializeField]
    private float range;//how close it will try to get to the player
    [SerializeField]
    private float projectileSpeed;//how fast the projectile will travel
    [SerializeField]
    private GameObject projectile; //the projectile it will shoot
    [SerializeField]
    private GameObject damageEffect;

    private float attackTimer = 0;
    Vector3 scale;

    private Transform player;
    private Vector3 directionVector;
    private float direction;
    PlayerStats playerStats;
    PlayerMovement playerMovement;
    private float distanceFromPlayer;
    private bool inRange;
    EnemyProjectile projectileStats;
    PauseController pauseController;

    int electrocuted = 0; //turns to 1 when electrocuted
    int frozen = 1; //stops the enemy from moving when frozen=1

    void Start()
    {
        player = GameObject.Find("Player").transform; //gets the location of the player
        pauseController = GameObject.Find("PauseController").GetComponent<PauseController>();
        scale = transform.localScale;
        directionCalc();
    }

    private void Update()
    {
        if (player != null)
        {
            distanceFromPlayer = (player.position - transform.position).magnitude; //calculates distance from the player
            if (distanceFromPlayer > range) //checks if enemy is within range of the player
            {
                directionCalc();
                movement();
            }
            else if(attackTimer<=0) { //if the player's in range and the timer=0
                attack();
            }
        }
        if (attackTimer >= 0)
        {
            attackTimer -= Time.deltaTime * pauseController.unpaused * frozen; //decreases timer proportionally to attack rate
        }
    }
    private void OnCollisionEnter2D(Collision2D collision)//gets called whenever there are collisions
    {
        if (collision.gameObject.CompareTag("Hammer"))//checks if it collides with a hammer
        {
            GameObject hmr = collision.gameObject;//the hammer game object
            Hammer hammer = hmr.GetComponent<Hammer>();//the code for the hammer script
            int hDamage = hammer.getDamage();
            damaged(hDamage);//damages the enemy based on the hammers damage
            Destroy(hmr);//destroys the hammer
        }
    }

    public void damaged(int hDamage)
    {
        health -= hDamage;//decreases health by the damage of the hammmer
        GameObject damageSprite = Instantiate(damageEffect);
        damageSprite.transform.position = transform.position;
        if (health <= 0)
        {
            GameObject plyr = GameObject.Find("Player"); //gets the player
            PlayerStats playerStats = plyr.GetComponent<PlayerStats>(); //gets the players stats
            playerStats.setEXP(playerStats.getEXP() + expYield); //increases the player's total exp by expYield
            Destroy(this.gameObject);//destroys this game object
        }
    }

    private void movement()
    {
        Vector2 pos = transform.position;

        if (directionVector.x > 0)
        {
            pos.x += speed * Mathf.Cos(direction) * Time.deltaTime * pauseController.unpaused * frozen;
            pos.y += speed * Mathf.Sin(direction) * Time.deltaTime * pauseController.unpaused * frozen;//calculates new location
            transform.localScale = new Vector3(scale.x, scale.y, scale.z);
        }
        else
        {
            pos.x += speed * -Mathf.Cos(direction) * Time.deltaTime * pauseController.unpaused * frozen;
            pos.y += speed * -Mathf.Sin(direction) * Time.deltaTime * pauseController.unpaused * frozen;
            transform.localScale = new Vector3(-scale.x, scale.y, scale.z); //flips it into the right direction
        }

        transform.position = pos;//sets new location
    }

    private void directionCalc()
    {
        directionVector = (player.transform.position - this.transform.position).normalized; //calculates the direction vector from the enemy to the player
        direction = Mathf.Atan(directionVector.y / directionVector.x); //calculate the angle it should travel at
    }

    private void attack()
    {
        GameObject Projectile = Instantiate(projectile); //creates the projectile
        projectileStats = Projectile.GetComponent<EnemyProjectile>();
        Vector2 projectilePos = transform.position;
        Projectile.transform.position = projectilePos;
        projectileStats.setEnemy(this.gameObject); //sets this as the projectile's enemy
        Projectile = null; //resets Porjectile and projectileStats
        projectileStats = null;
        attackTimer += attackRate;//restarts timer until it can attack
    }
    
    public float getProjectileSpeed() { 
        return projectileSpeed;
    }

    public int getDamage()
    {
        return damage;
    }

    public float getRange() {
        return range;
    }

    public void electrocute() {
        if (electrocuted != 1) { //checks if the enemy has been electrocuted before
            frozen = 0; //freezes the enemy
            Invoke("unfreeze", 1.5f); //unfreezes the enemy in 0.8 seconds
        }
        electrocuted = 1;
        Invoke("unelectrocute", 3f);
    }

    private void unelectrocute()
    {
        electrocuted = 0;
    }

    public int getElectrocuted()
    {
        return electrocuted;
    }

    public void unfreeze()
    {
        frozen = 1;
    }

    public float getSpeed()
    {
        return speed;
    }

    public float getRate()
    {
        return attackRate;
    }

    public int getHealth()
    {
        return health;
    }
}
