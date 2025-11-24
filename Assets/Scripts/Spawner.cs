using System.Collections;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    [SerializeField]
    private GameObject[] spawnTable;//the enemies it can spawn

    [SerializeField]
    private int[] spawnMaxCounts;//the max quanitity it will spawn

    [SerializeField]
    private int[] spawnMinCounts;//the min quanitity it will spawn

    [SerializeField]
    private float spawnRate;//how fast it will spawn enemies

    [SerializeField]
    private float timeUntilSpawn;//when it will start spawning

    private GameObject player;
    private int randomSpawnLocationNumber;
    private GameObject timer;
    private Timer timerTimer;
    PauseController pauseController;

    private void Start()
    {
        pauseController = GameObject.Find("PauseController").GetComponent<PauseController>();
        player = GameObject.Find("Player");
        timer = GameObject.Find("Timer");
        timerTimer = timer.GetComponent<Timer>();
        StartCoroutine(Spawning());
    }

    IEnumerator Spawning() {
        while (player!=null) 
        {
            yield return new WaitForSeconds(spawnRate); //makes it wait for the spawn rate before 
            if (timerTimer.getTime() < 300-timeUntilSpawn && pauseController.unpaused==1)
            {
                Vector2 newSpawnerPos = player.transform.position; //new position is the player's position
                randomSpawnLocationNumber = Random.Range(0, 4); //randomly selects what side it will spawn on
                if (randomSpawnLocationNumber == 0)
                {
                    newSpawnerPos.x += 15; //spawns to the left of the player
                    newSpawnerPos.y += Random.Range(-10f, 10f);
                }
                else if (randomSpawnLocationNumber == 1)
                {
                    newSpawnerPos.x -= 15; //spawns to the right of the player
                    newSpawnerPos.y += Random.Range(-10f, 10f);
                }
                else if (randomSpawnLocationNumber == 2)
                {
                    newSpawnerPos.y -= 10; //spawns below the player
                    newSpawnerPos.x += Random.Range(-15f, 15f);
                }
                else
                {
                    newSpawnerPos.y += 10; //spawns above the player
                    newSpawnerPos.x += Random.Range(-15f, 15f);
                }
                transform.position = newSpawnerPos; //sets spawner to new player position

                int randomEnemyNum = Random.Range(0, spawnTable.Length);
                for (int i = 0; i < Random.Range(spawnMinCounts[randomEnemyNum], spawnMaxCounts[randomEnemyNum] + 1); i++)
                {
                    GameObject spawn = Instantiate(spawnTable[randomEnemyNum]); //creates a random enemy from the spawn table
                    Vector2 newEnemyPos = newSpawnerPos;
                    newEnemyPos.x = newSpawnerPos.x + Random.Range(-1f, 1f); //sets it to a random position around the spawner
                    newEnemyPos.y = newSpawnerPos.y + Random.Range(-1f, 1f);
                    spawn.transform.position = newEnemyPos;
                }
            }
        }
    }
}
