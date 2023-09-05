using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    public List<GameObject> rightSideSpawnPoints;
    public List<GameObject> leftSideSpawnPoints;
    public List<GameObject> leftMovingGameObjects;
    public List<GameObject> rightMovingGameObjects;
    private const float spawnRate = 1.2f;
    private bool gameIsActive;
    
    [HideInInspector]
    public static bool paused;
    
    void Start()
    {
        gameIsActive = true;
        StartCoroutine(spawnEntities());
    }

    private void SpawnLeftMovingEntity()
    {
        if (!gameIsActive) return;
        
        var index = Random.Range(0, rightSideSpawnPoints.Count);
        var spawnPoint = rightSideSpawnPoints[index];

        index = Random.Range(0, leftMovingGameObjects.Count);
        var entity = leftMovingGameObjects[index];

        Instantiate(entity, spawnPoint.transform.position, spawnPoint.transform.rotation);
    }

    private void SpawnRightMovingEntity()
    {
        if (!gameIsActive) return;
        
        var index = Random.Range(0, leftSideSpawnPoints.Count);
        var spawnPoint = leftSideSpawnPoints[index];

        index = Random.Range(0, rightMovingGameObjects.Count);
        var entity = rightMovingGameObjects[index];

        Instantiate(entity, spawnPoint.transform.position, spawnPoint.transform.rotation);
    }

    private IEnumerator spawnEntities()
    {
        while (gameIsActive)
        {
            yield return new WaitForSeconds(spawnRate);
            if (paused)
            {
                continue;
            }

            var rightSideSpawn = (Random.Range(0, 2) == 0);

            if (rightSideSpawn)
            {
                SpawnLeftMovingEntity();
            }
            else
            {
                SpawnRightMovingEntity();
            }
        }
    }
}
