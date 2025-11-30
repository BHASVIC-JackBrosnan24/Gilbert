
using UnityEngine;


public class AllyScript : MonoBehaviour
{
    GameObject target;

    [SerializeField]
    private int health; //how much damage the ally can take
    [SerializeField]
    private int damage; //how much ally the enemy deals to the player
    [SerializeField]
    private float speed; //how fast the ally will move
    [SerializeField]
    private float attackRate; //how fast the ally will attack

    private GameObject damageEffect;

    ParentMeleeEnemy melee;
    ParentRangedEnemy ranged;
    PauseController pauseController;
    SpriteRenderer spriteRenderer;

    Rigidbody2D rigidBody;
    CapsuleCollider2D capsuleCollider;

    private Vector3 directionVector;
    private float direction;
    private Vector3 scale;
    private float attackTimer = 0;
    void Start()
    {
        rigidBody = GetComponent<Rigidbody2D>();
        capsuleCollider = GetComponent<CapsuleCollider2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        target = GameObject.FindWithTag("Enemy");
        melee = gameObject.GetComponent<ParentMeleeEnemy>();
        ranged = gameObject.GetComponent<ParentRangedEnemy>();
        scale = transform.localScale;
        pauseController = GameObject.Find("PauseController").GetComponent<PauseController>(); //Gets the pause controller
        damageEffect = GameObject.FindWithTag("Player").GetComponent<PlayerStats>().getDE();

        if (melee != null) { //sets all the stats if this enemy is melee
            health = melee.getHealth();
            damage = melee.getDamage();
            speed = melee.getSpeed();
            attackRate = melee.getRate();
            spriteRenderer.color = new Color(200, 0, 200);
            Destroy(melee); //destroys the enemy script, so it functionally is no longer an enemy

            rigidBody.includeLayers = LayerMask.GetMask("Enemy","EnemyProjectile");
            rigidBody.excludeLayers = LayerMask.GetMask("Background", "Default", "Effects", "Hammer", "PowerUpGem", "Water", "UI");
            capsuleCollider.contactCaptureLayers = LayerMask.GetMask("Enemy", "EnemyProjectile");
            capsuleCollider.callbackLayers = LayerMask.GetMask("Enemy", "EnemyProjectile");
            capsuleCollider.forceSendLayers = LayerMask.GetMask("Enemy", "EnemyProjectile");
            capsuleCollider.forceReceiveLayers = LayerMask.GetMask("Enemy", "EnemyProjectile");
            capsuleCollider.includeLayers = LayerMask.GetMask("Enemy", "EnemyProjectile");
            capsuleCollider.excludeLayers = LayerMask.GetMask("Background", "Default", "Effects", "Hammer", "PowerUpGem", "Water", "UI");
        }
        else if (ranged != null) //sets all the stats if the enemy is ranged
        {
            health = ranged.getHealth();
            damage = ranged.getDamage();
            speed = ranged.getSpeed();
            attackRate = ranged.getRate();
            Destroy(ranged);
            spriteRenderer.color = new Color(200, 0, 200);

            rigidBody.includeLayers = LayerMask.GetMask("Enemy", "EnemyProjectile");
            rigidBody.excludeLayers = LayerMask.GetMask("Background", "Default", "Effects", "Hammer", "PowerUpGem", "Water", "UI");
            capsuleCollider.contactCaptureLayers = LayerMask.GetMask("Enemy", "EnemyProjectile");
            capsuleCollider.callbackLayers = LayerMask.GetMask("Enemy", "EnemyProjectile");
            capsuleCollider.forceSendLayers = LayerMask.GetMask("Enemy", "EnemyProjectile");
            capsuleCollider.forceReceiveLayers = LayerMask.GetMask("Enemy", "EnemyProjectile");
            capsuleCollider.includeLayers = LayerMask.GetMask("Enemy", "EnemyProjectile");
            capsuleCollider.excludeLayers = LayerMask.GetMask("Background", "Default", "Effects", "Hammer", "PowerUpGem", "Water", "UI");
        }
        gameObject.layer = 7; //sets the object to layer 7 - the player layer
        gameObject.tag = "Untagged"; //removes the enemy tag
    }

    void Update()
    {
        if (target == null) {
                target = GameObject.FindWithTag("Enemy");
        }
        else if (target.gameObject.CompareTag("Enemy") == false)
        {
            target = null;
            target = GameObject.FindWithTag("Enemy");
        }
        else
        {
            if (transform.position != target.transform.position) //makes sure the enemy isn't at the same location as the player
            {
                directionCalc();
                movement();
            }
        }
        if (attackTimer > 0) { 
            attackTimer -= Time.deltaTime * attackRate * pauseController.unpaused;
        }
    }

    private void OnCollisionStay2D(Collision2D collision) //every frame that there are collisions
    {
        if (collision.gameObject.CompareTag("Enemy"))
        { 
            GameObject enemy = collision.gameObject;
            attack(enemy); //passes in the enemy game object to the attack method
        }
    }

    private void attack(GameObject enemy)
    {
        melee = enemy.GetComponent<ParentMeleeEnemy>();
        ranged = enemy.GetComponent<ParentRangedEnemy>();
        if (attackTimer <= 0) 
        {
            if (melee != null)
            {
                melee.damaged(damage); //damages melee enemy
                attackTimer += 1;
            }
            if (ranged != null) { 
                ranged.damaged(damage); //damages ranged enemy
                attackTimer += 1;
            }
        }
    }

    private void movement()
    {
        Vector2 pos = transform.position;

        if (directionVector.x > 0)
        {
            pos.x += speed * Mathf.Cos(direction) * Time.deltaTime * pauseController.unpaused;
            pos.y += speed * Mathf.Sin(direction) * Time.deltaTime * pauseController.unpaused;//calculates new location
            transform.localScale = new Vector3(scale.x, scale.y, scale.z);
        }
        else
        {
            pos.x += speed * -Mathf.Cos(direction) * Time.deltaTime * pauseController.unpaused;
            pos.y += speed * -Mathf.Sin(direction) * Time.deltaTime * pauseController.unpaused;
            transform.localScale = new Vector3(-1 * scale.x, scale.y, scale.z);
        }

        transform.position = pos;//sets new location
    }

    private void directionCalc()
    {
        directionVector = (target.transform.position - this.transform.position).normalized; //calculates the direction vector from the enemy to the player
        direction = Mathf.Atan(directionVector.y / directionVector.x); //calculate the angle it should travel at
    }

    public void damaged(int damaged)
    {
        health-=damaged;
        GameObject damageSprite = Instantiate(damageEffect);
        damageSprite.transform.position = transform.position;
        if (health < 0)
        {
            Destroy(gameObject);
        }
    }
}
