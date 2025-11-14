using UnityEngine;

public class FireTrailScript : MonoBehaviour
{

    private float timer; //timer until it despawns
    float maxTimer;
    float opacity = 1; //opacity
    SpriteRenderer spriteRenderer;
    PauseController pauseController;
    PlayerStats playerStats;
    float attackTimer = 0.25f;//cooldown between damage ticks
    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>(); //gets the sprite renderer
        pauseController = GameObject.Find("PauseController").GetComponent<PauseController>(); //gets the pause controller
        playerStats = GameObject.FindWithTag("Player").GetComponent<PlayerStats>(); //gets the player's stats
        timer = playerStats.getFireTrail(); //sets the max time equal to the value of fire trail
        maxTimer = timer;
    }

    void Update()
    {
        spriteRenderer.color = new Color(1f, 1f, 1f, opacity); //sets the opacity to the new opacity
        opacity = opacity - ((Time.deltaTime / maxTimer) * pauseController.unpaused); //decreases opacity
        timer = timer - (Time.deltaTime * pauseController.unpaused); //decreases timer
        if (timer <= 0)
        {
            Destroy(gameObject); //destroys game object if the timer runs out
        }
        if (attackTimer > 0) {  //decreases attack timer
            attackTimer-= Time.deltaTime;
            print(4);
        }
    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Enemy")) //if it touches an enemy
        {
            ParentMeleeEnemy melee = collision.gameObject.GetComponent<ParentMeleeEnemy>();  //gets the melee enemy script
            ParentRangedEnemy ranged = collision.gameObject.GetComponent<ParentRangedEnemy>(); //gets the ranged enemy script
            print(3);

            if (melee != null && attackTimer <=0) {
                melee.damaged(playerStats.getFireTrail());
                print(2);
                attackTimer = 0.25f;
            }
            else if (ranged != null&&attackTimer<=0) {
                ranged.damaged(playerStats.getFireTrail());
                print(1);
                attackTimer = 0.25f;
            }
        }
    }
}
