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

    private void Start()
    {
        player = GameObject.Find("Player");
        StartCoroutine(Spawning());
    }

    IEnumerator Spawning() {
        while (player!=null) 
        {
            yield return new WaitForSeconds(5/spawnRate); //makes it wait for the spawn rate before 
            Vector2 newSpawnerPos = player.transform.position; //new position is the player's position
            randomSpawnLocationNumber = Random.Range(0, 4); //randomly selects what side it will spawn on
            if (randomSpawnLocationNumber == 0)
            {
                newSpawnerPos.x += 15; //spawns to the left of the player
                newSpawnerPos.y += Random.Range(-10f, 10f);
            }
            else if (randomSpawnLocationNumber == 1) {
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

            GameObject spawn = Instantiate(spawnTable[Random.Range(0, spawnTable.Length)]); //creates a random enemy from the spawn table
            Vector2 newEnemyPos = newSpawnerPos;
            newEnemyPos.x = newSpawnerPos.x + Random.Range(-1f, 1f); //sets it to a random position around the spawner
            newEnemyPos.y = newSpawnerPos.y + Random.Range(-1f, 1f);
            spawn.transform.position = newEnemyPos;
        }
    }
}
