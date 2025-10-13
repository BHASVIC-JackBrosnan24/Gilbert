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

    private Transform player;
    private Vector3 directionVector;
    private float direction;

    void Start()
    {
        player = GameObject.Find("Player").transform; //gets the location of the player
        directionCalc();
    }

    private void Update()
    {
        if (transform.position != player.transform.position) //makes sure the enemy isn't at the same location as the player
        {
            directionCalc();
            movement();
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

        if (collision.gameObject == player) {
            attack();
        }
    }

    private void damaged(int hDamage)
    {
        health -= hDamage;//decreases health by the damage of the hammmer
        if (health <= 0)
        {
            Destroy(this.gameObject);//destroys this game object
        }
    }

    private void movement() {
        Vector2 pos = transform.position;

        if (directionVector.x > 0)
        {
            pos.x += speed * Mathf.Cos(direction) * Time.deltaTime;
            pos.y += speed * Mathf.Sin(direction) * Time.deltaTime;//calculates new location
        }
        else
        {
            pos.x += speed * -Mathf.Cos(direction) * Time.deltaTime;
            pos.y += speed * -Mathf.Sin(direction) * Time.deltaTime;
        }

        transform.position = pos;//sets new location
    }

    private void directionCalc()
    {
        directionVector = (player.transform.position - this.transform.position).normalized; //calculates the direction vector from the enemy to the player
        direction = Mathf.Atan(directionVector.y / directionVector.x); //calculate the angle it should travel at
    }

    private void attack() { }
}
