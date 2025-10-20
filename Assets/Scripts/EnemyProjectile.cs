using UnityEngine;

public class EnemyProjectile : MonoBehaviour
{
    int damage; //the amount of damage the projectile will do
    float speed; //how fast the projectile should travel
    float direction; //the direction the projectile will travel in
    float range; //how far the projectile will travel 

    private GameObject player;

    private GameObject enemy;

    private ParentRangedEnemy enemyStats;

    private Vector3 directionVector; //the direction vector of the direction it should travel in
    private bool ready=false;//checks if setEnemy() has happened
    float timer;

    public void setEnemy(GameObject thisEnemy)
    {
        enemy = thisEnemy;
        enemyStats = enemy.GetComponent<ParentRangedEnemy>();
        player = GameObject.Find("Player");
        speed = enemyStats.getProjectileSpeed(); //gets the projectile speed from the stats script
        damage = enemyStats.getDamage(); //gets the damage from the stats script
        range = enemyStats.getRange();
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
                pos.x += speed * Mathf.Cos(direction) * Time.deltaTime;
                pos.y += speed * Mathf.Sin(direction) * Time.deltaTime; //calculates the new location
            }
            else
            {
                pos.x += speed * -Mathf.Cos(direction) * Time.deltaTime;
                pos.y += speed * -Mathf.Sin(direction) * Time.deltaTime;
            }

            transform.position = pos; //sets the new position for the projectile

            timer=timer-Time.deltaTime;
            if (timer <= 0) { 
                Destroy(gameObject);
            }
        }
    }
    private void directionCalc()
    { //method for calculating the direction
        directionVector = (player.transform.position - enemy.transform.position).normalized; //calculates the direction vector
        direction = Mathf.Atan(directionVector.y / directionVector.x); //calculates the angle for the direction the projectile shoould travel in
    }
}
