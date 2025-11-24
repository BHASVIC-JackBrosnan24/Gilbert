using UnityEngine;
using UnityEngine.UIElements;

public class ZeusScript : MonoBehaviour
{
    [SerializeField]
    GameObject zeusLaser;

    [SerializeField]
    GameObject lightningBolt;

    [SerializeField]
    GameObject soldier;

    [SerializeField]
    GameObject lightningChariot;

    Transform player;
    PauseController pauseController;

    [SerializeField]
    float laserSpeed;
    [SerializeField]
    float lightningSpeed;

    float laserDistance = 14.5f;
    float lightningDistance = 100f;

    [SerializeField]
    int laserDamage;
    [SerializeField]
    int lightningDamage;
    

    float lightningTimer=3; //timer until he throws lightning
    float laserTimer=8; //timer until he fires lasers
    float chariotTimer=12; //timer until he spawns the chariot
    float defenderTimer=3; ////timer until he spawns defenders
    void Start()
    {
        player = GameObject.Find("Player").transform; //gets the location of the player
        pauseController = GameObject.Find("PauseController").GetComponent<PauseController>();
    }

    // Update is called once per frame
    void Update()
    {
        lightningTimer -= Time.deltaTime*pauseController.unpaused;
        laserTimer -= Time.deltaTime * pauseController.unpaused;
        chariotTimer -= Time.deltaTime * pauseController.unpaused;
        defenderTimer -= Time.deltaTime * pauseController.unpaused;

        if (lightningTimer <= 0) {
            throwLightning();
            lightningTimer = 4;
        }
        if (laserTimer <= 0 && (player.position - transform.position).magnitude < 15) //checks if you are in range
        {
            Invoke("fireLasers", 0.1f);
            Invoke("fireLasers", 0.12f);
            Invoke("fireLasers", 0.14f);
            Invoke("fireLasers", 0.16f);
            Invoke("fireLasers", 0.18f);
            Invoke("fireLasers", 0.2f);
            Invoke("fireLasers", 0.22f);
            Invoke("fireLasers", 0.24f);
            laserTimer = 8.5f;
        }
        if (defenderTimer <= 0) {
            for (int i = 0; i < Random.Range(3, 10); i++) { //repeats a random number of times
                Invoke("spawnDefender",i/3);
            }
            defenderTimer = 12;
        }
        if (chariotTimer <= 0) {
            spawnChariot();
            chariotTimer = 15;
        }
    }

    private void spawnDefender() {
        GameObject spawn = Instantiate(soldier);
        soldier.transform.position = new Vector3(transform.position.x + Random.Range(-0.5f, 0.5f), transform.position.y + Random.Range(-0.5f, 0.5f), transform.position.z);
    }

    private void spawnChariot() { 
        GameObject chariot = Instantiate(lightningChariot);
        chariot.transform.position = transform.position;
    }

    private void throwLightning() { 
        GameObject lightning = Instantiate(lightningBolt);
        EnemyProjectile projectileStats = lightning.GetComponent<EnemyProjectile>();
        Vector2 projectilePos = transform.position;
        lightning.transform.position = projectilePos;
        projectileStats.setEnemy(this.gameObject); //sets this as the projectile's enemy
        lightning = null; //resets Porjectile and projectileStats
        projectileStats = null;
    }

    private void fireLasers() {
        GameObject laser = Instantiate(zeusLaser);
        EnemyProjectile projectileStats = laser.GetComponent<EnemyProjectile>();
        Vector2 projectilePos = transform.position;
        laser.transform.position = projectilePos;
        projectileStats.setEnemy(this.gameObject); //sets this as the projectile's enemy
        laser = null; //resets Porjectile and projectileStats
        projectileStats = null;
    }

    public float getProjectileSpeed(int what) {
        if (what == 0) { return laserSpeed; }
        else return lightningSpeed;
    }

    public int getDamage(int what)
    {
        if (what == 0) { return laserDamage; }
        else return lightningDamage;
    }

    public float getRange(int what) { 
        if(what == 0) {return laserDistance; }
        else return lightningDistance;
    }
}
