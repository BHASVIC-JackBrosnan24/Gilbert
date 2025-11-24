using TMPro;
using UnityEngine;

public class ParentMeleeEnemy : MonoBehaviour
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
    private GameObject damageEffect;

    private float attackTimer = 0;

    private Transform player;
    private Vector3 directionVector;
    private float direction;
    PlayerStats playerStats;
    PlayerMovement playerMovement;
    Vector3 scale;

    PauseController pauseController;

    int electrocuted = 0; //turns to 1 when electrocuted
    int frozen = 1; //stops the enemy from moving 

    void Start()
    {
        player = GameObject.Find("Player").transform; //gets the location of the player
        directionCalc();
        pauseController = GameObject.Find("PauseController").GetComponent<PauseController>(); //Gets the pause controller
        scale = transform.localScale;
    }

    private void Update()
    {
        if (player != null)
        {
            if (transform.position != player.transform.position) //makes sure the enemy isn't at the same location as the player
            {
                directionCalc();
                movement();
            }
        }
        if (attackTimer >= 0) { 
        attackTimer -= Time.deltaTime*attackRate * pauseController.unpaused * frozen; //decreases timer proportionally to attack rate
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

    private void OnCollisionStay2D(Collision2D collision) //every frame that there are collisions
    {
        if (collision.gameObject.CompareTag("Player"))
        { //if it collides with the player
            GameObject plyr = collision.gameObject;
            attack(plyr); //passes in the player game object to the attack method
        }
    }

    public void damaged(int hDamage)
    {
        health -= hDamage;//decreases health by the damage of the hammmer
        if (health <= 0)
        {
            GameObject plyr = GameObject.Find("Player"); //gets the player
            PlayerStats playerStats = plyr.GetComponent<PlayerStats>(); //gets the players stats
            playerStats.setEXP(playerStats.getEXP() + expYield); //increases the player's total exp by expYield
            GameObject damageSprite = Instantiate(damageEffect);
            damageSprite.transform.position = transform.position;
            Destroy(this.gameObject);//destroys this game object
        }
    }

    private void movement() {
        Vector2 pos = transform.position;

        if (directionVector.x > 0)
        {
            pos.x += speed * Mathf.Cos(direction) * Time.deltaTime * pauseController.unpaused * frozen;
            pos.y += speed * Mathf.Sin(direction) * Time.deltaTime * pauseController.unpaused * frozen;//calculates new location
            transform.localScale = new Vector3( scale.x, scale.y, scale.z );
        }
        else
        {
            pos.x += speed * -Mathf.Cos(direction) * Time.deltaTime * pauseController.unpaused * frozen;
            pos.y += speed * -Mathf.Sin(direction) * Time.deltaTime * pauseController.unpaused * frozen;
            transform.localScale = new Vector3(-1 * scale.x, scale.y, scale.z);
        }

        transform.position = pos;//sets new location
    }

    private void directionCalc()
    {
        directionVector = (player.transform.position - this.transform.position).normalized; //calculates the direction vector from the enemy to the player
        direction = Mathf.Atan(directionVector.y / directionVector.x); //calculate the angle it should travel at
    }

    private void attack(GameObject plyr) {
        playerStats = plyr.GetComponent<PlayerStats>(); //gets the stats and movement components
        playerMovement = plyr.GetComponent<PlayerMovement>();
        if (playerMovement.getDashInvincibility() == false && attackTimer<=0) //makes sure the player isn't dashing (invincible), and enemy can attack
        {
            playerStats.damaged(damage); //damages player
            attackTimer += 1;
        }
    }
    public void electrocute()
    {
        if (electrocuted != 1)
        { //checks if the enemy has been electrocuted before
            frozen = 0; //freezes the enemy
            Invoke("unfreeze", 0.8f); //unfreezes the enemy in 0.8 seconds
        }
        electrocuted = 1;
        Invoke("unelectrocute", 2.5f);
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
}
