using UnityEditor.Experimental.GraphView;
using UnityEngine;
using UnityEngine.InputSystem;

public class Hammer : MonoBehaviour
{
    int damage; //the amount of damage the hammer will do
    float speed; //how fast the hammer should travel
    float direction; //the direction the hammer will travel in
    private Camera mainCamera;

    private GameObject player;

    private PlayerStats playerStats;
    PauseController pauseController;

    private Vector3 directionVector; //the direction vector of the direction it should travel in
    [SerializeField]
    private float timer; //a timer for when the hammer should despawn

    public void Start()
    {
        pauseController = GameObject.Find("PauseController").GetComponent<PauseController>();
        mainCamera = Camera.main;
        player = GameObject.Find("Player");
        playerStats = player.GetComponent<PlayerStats>();
        speed = playerStats.getHammerSpeed(); //gets the hammer speed from the stats script
        damage = playerStats.getDamage(); //gets the damage from the stats script
        directionCalc(); 
    }

    private void Update()
    {
        Vector2 pos = transform.position;

        if (directionVector.x > 0) //checks if it is moving in a positive direction
        {
            pos.x += speed * Mathf.Cos(direction) * Time.deltaTime * pauseController.unpaused;
            pos.y += speed * Mathf.Sin(direction) * Time.deltaTime * pauseController.unpaused; //calculates the new location
        }
        else {
            pos.x += speed * -Mathf.Cos(direction) * Time.deltaTime * pauseController.unpaused;
            pos.y += speed * -Mathf.Sin(direction) * Time.deltaTime * pauseController.unpaused;
        }

        timer=timer - 1*(Time.deltaTime * pauseController.unpaused); //decreases the timer
        if (timer <= 0)
        {
            Destroy(this.gameObject);//destorys the hammer
        }

            transform.position = pos; //sets the new position for the hammer

        transform.Rotate(0, 0, -720 * Time.deltaTime * pauseController.unpaused); //rotates the hammer by 360 every second
    }

    public int getDamage()
    {
        return damage;
    }

    private void directionCalc() { //method for calculating the direction
        Vector3 mouse = mainCamera.ScreenToWorldPoint(Input.mousePosition); //vector 3 for where the mouse looks like it should be
        directionVector = (mouse - player.transform.position).normalized; //calculates the direction vector
        direction=Mathf.Atan(directionVector.y/directionVector.x); //calculates the angle for the direction the hammer shoould travel in
    }

}
