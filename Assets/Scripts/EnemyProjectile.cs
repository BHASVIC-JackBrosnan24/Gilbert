using UnityEngine;
using UnityEngine.UIElements;

public class EnemyProjectile : MonoBehaviour
{
    int damage; //the amount of damage the projectile will do
    float speed; //how fast the projectile should travel
    float direction; //the direction the projectile will travel in
    float range; //how far the projectile will travel 

    [SerializeField]
    int laserOrLightning;

    private GameObject player;

    private GameObject enemy;

    private ParentRangedEnemy enemyStats;

    private PlayerStats playerStats;
    private PlayerMovement playerMovement;
    PauseController pauseController;

    private Vector3 directionVector; //the direction vector of the direction it should travel in
    private bool ready=false;//checks if setEnemy() has happened
    float timer;

    public void setEnemy(GameObject thisEnemy)
    {
        pauseController = GameObject.Find("PauseController").GetComponent<PauseController>();
        enemy = thisEnemy;
        enemyStats = enemy.GetComponent<ParentRangedEnemy>();
        if (enemyStats == null)
        {
            ZeusScript zeusStats = enemy.GetComponent<ZeusScript>();
            damage = zeusStats.getDamage(laserOrLightning);
            speed = zeusStats.getProjectileSpeed(laserOrLightning);
            range = zeusStats.getRange(laserOrLightning);
        }
        else
        {
            speed = enemyStats.getProjectileSpeed(); //gets the projectile speed from the stats script
            damage = enemyStats.getDamage(); //gets the damage from the stats script
            range = enemyStats.getRange();
        }
        player = GameObject.Find("Player");
        ready = true;
        timer = 1.2f*(range / speed); //uses time=distance/speed to calc how long it should travel for, plus a little extra
        directionCalc();
    }

    private void Update()
    {
        if (ready)
        {
            Vector2 pos = transform.position;

            if (directionVector.x > 0) //checks if it is moving in a positive direction
            {
                pos.x += speed * Mathf.Cos(direction) * Time.deltaTime * pauseController.unpaused;
                pos.y += speed * Mathf.Sin(direction) * Time.deltaTime * pauseController.unpaused; //calculates the new location
            }
            else
            {
                pos.x += speed * -Mathf.Cos(direction) * Time.deltaTime * pauseController.unpaused;
                pos.y += speed * -Mathf.Sin(direction) * Time.deltaTime * pauseController.unpaused;
            }

            transform.position = pos; //sets the new position for the projectile

            timer=timer-(Time.deltaTime * pauseController.unpaused);
            if (timer <= 0) { 
                Destroy(gameObject);
            }
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player")) { 
            GameObject plyr = collision.gameObject; //takes the player as a game object
            playerStats = plyr.GetComponent<PlayerStats>(); //gets the stats and movement components
            playerMovement = plyr.GetComponent<PlayerMovement>();
            if (playerMovement.getDashInvincibility() == false) //makes sure the player isn't dashing (invincible), and enemy can attack
            {
                playerStats.damaged(damage); //damages player
                Destroy(gameObject);
            }
        }
    }
    private void directionCalc()
    { //method for calculating the direction
        directionVector = (player.transform.position - enemy.transform.position).normalized; //calculates the direction vector
        direction = Mathf.Atan(directionVector.y / directionVector.x); //calculates the angle for the direction the projectile shoould travel in
        this.transform.Rotate(0,0,180*direction/Mathf.PI); //rotates the angle
        if (player.transform.position.x < transform.position.x) {
            Vector3 scale = transform.localScale;
            transform.localScale = new Vector3(-scale.x, scale.y, scale.z);
        }
    }
}
