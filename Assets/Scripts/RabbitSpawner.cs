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
        if(GameManager.Instance.currentSpawnedRabbits.Count < 3)
        {
            int spawnPointIndex = Random.Range(0, spawnPoints.Count - 1);
            GameObject newRabbit = GameObject.Instantiate(rabbitPrefab, spawnPoints[spawnPointIndex].position, transform.rotation);
        }
    }
}
