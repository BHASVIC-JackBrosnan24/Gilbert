using Unity.VisualScripting;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    private float speed; //base speed

    private float speedV; //vertical speed
    private float speedH; //horizontal speed

    private float tempH; //tempoary horizontal speed
    private float tempV; //temporary veritical speed

    private float dashing = 0f; //value that is >0 when dashing
    private float dashCooldown = 0f; //cooldown for the dash

    private SpriteRenderer spriteRenderer;
    private PlayerStats playerStats;
    PauseController pauseController;

    [SerializeField]
    private Sprite dashingSprite;

    [SerializeField]
    private Sprite regularSprite;

    [SerializeField]
    GameObject fireTrail; //the fireTrail gameObject

    private void Start()
    {
        playerStats = this.GetComponent<PlayerStats>();
        speed = playerStats.getSpeed(); // gets player speed from the stats script
        spriteRenderer = GetComponent<SpriteRenderer>();
        pauseController = GameObject.Find("PauseController").GetComponent<PauseController>();
    }
    private void Update()
    {
        speedV = speed; 
        speedH = speed; 
        float h = Input.GetAxis("Horizontal"); //equals 1 if A or D are pressed
        float v = Input.GetAxis("Vertical"); //equals 1 if W or S are pressed
        Vector2 pos = transform.position;

        if (dashing > 0f)
        {
            speed = speed / 3;
        }

        if (Input.GetKeyDown("space") && dashCooldown<=0f) //checks if you can dash
        {
            dash();
        }

        if (dashing > 0f)
        {
            dashing = dashing - Time.deltaTime * pauseController.unpaused;
            h = tempH; //makes sure h and v don't change for the entirety of the dash
            v = tempV;
            spriteRenderer.sprite = dashingSprite; //changes sprite so it looks like you are dashing
        }
        else { 
            tempH = h; //makes sure when you dash, h and v will be the correct values
            tempV = v;
            spriteRenderer.sprite = regularSprite; //changes sprite back to the regular sprite
        }

        if (dashCooldown > 0f) {
            dashCooldown = dashCooldown - Time.deltaTime * pauseController.unpaused;//decreases dash timer
        }

        if (dashing > 0f)
        {
            speed = speed * 3;
        }

        if ((h ==1 && v ==1) || (h == 1 && v == -1) || (h == -1 && v == 1) || (h == -1 && v == -1))//checks if you are moving diagonally
        {
            speedV = Mathf.Sqrt(speed * speed * 0.5f);//calculates the correct speed if you move diagonally
            speedH = Mathf.Sqrt(speed * speed * 0.5f);
        }

        if (h != 0 && v != 0 && dashing > 0)
        {
            if (v < 0)
            {
                v = -1;
            }
            else {
                v = 1;
            }
            if (h < 0)
            {
                h = -1;
            }
            else
            {
                h = 1;
            }
        }
        else if (v != 0 && dashing > 0)
        {
            if (v < 0)
            {
                v = -1;
            }
            else
            {
                v = 1;
            }
        }
        else if (h != 0 && dashing > 0)
        {
            if (h < 0)
            {
                h = -1;
            }
            else
            {
                h = 1;
            }
        }
        else if (h == 0 && v == 0 && dashing > 0) {
            h = 1;
        }



            pos.x += h * speedH * Time.deltaTime * pauseController.unpaused; //calculates your new location
        pos.y += v * speedV * Time.deltaTime * pauseController.unpaused;


        transform.position = pos;
        
    }

    private void dash() { //all the code that needs to be called once to setS up the dash
        dashing = 0.2f; 
        dashCooldown = 0.65f;
        if (playerStats.getFireTrail() > 0) {
            Invoke("spawnFire", 0.03f); //spawns a fireTrail after short intervals
            Invoke("spawnFire", 0.07f);
            Invoke("spawnFire", 0.1f);
            Invoke("spawnFire", 0.13f);
            Invoke("spawnFire", 0.17f);
        }
    }

    public bool getDashInvincibility() { //true if you are dashing, hence should be invincible
        if(dashing > 0f){
            return true;
        }
        else{  
            return false; 
        }
            
    }

    private void spawnFire() { 
        GameObject fire = Instantiate(fireTrail); //spawns fireTrail at the current location
        fire.transform.position = transform.position;
    }

}
