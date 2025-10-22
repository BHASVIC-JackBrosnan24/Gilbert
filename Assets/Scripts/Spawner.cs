using System.Collections;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    [SerializeField]
    private GameObject[] spawnTable;//the enemies it can spawn

    [SerializeField]
    private GameObject[] spawnMaxCounts;//the max quanitity it will spawn

    [SerializeField]
    private GameObject[] spawnMinCounts;//the min quanitity it will spawn

    [SerializeField]
    private float spawnRate;//how fast it will spawn enemies

    [SerializeField]
    private float timeUntilSpawn;//when it will start spawning

    private void Start()
    {
        StartCoroutine(Spawning());
    }

    IEnumerator Spawning() {
        while (true) {
            GameObject spawn = Instantiate(spawnTable[Random.Range(0, spawnTable.Length)]); //creates a random enemy from the spawn table
            Vector2 newPos = transform.position;
            newPos.x = transform.position.x + Random.Range(-1f, 1f); //sets it to a random position around the spawner
            newPos.x = transform.position.y + Random.Range(-1f, 1f);
            spawn.transform.position = newPos;
            yield return spawnRate; 
        }
    }
}
