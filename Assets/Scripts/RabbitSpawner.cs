using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RabbitSpawner : MonoBehaviour
{
    public GameObject rabbitPrefab;
    public List<Transform> spawnPoints;

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(spawnRabbitsCoroutine());
    }

    public IEnumerator spawnRabbitsCoroutine()
    {
        yield return new WaitForSeconds(5f);
        int chanceToSpawn = Random.Range(0, 100);
        Debug.Log("Random chance to spawn is " + chanceToSpawn);
        if(GameManager.Instance.currentSpawnedRabbits.Count < 3)
        {
            if(chanceToSpawn < 50)
            {
                int spawnPointIndex = Random.Range(0, spawnPoints.Count - 1);
                GameObject newRabbit = GameObject.Instantiate(rabbitPrefab, spawnPoints[spawnPointIndex].position, transform.rotation);
                GameManager.Instance.currentSpawnedRabbits.Add(newRabbit);
            }
            StartCoroutine(spawnRabbitsCoroutine());
        }
    }
}
