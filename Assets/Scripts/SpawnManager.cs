using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    public List<GameObject> rightSideSpawnPoints;
    public List<GameObject> leftSideSpawnPoints;
    public List<GameObject> leftMovingGameObjects;
    public List<GameObject> rightMovingGameObjects;
    public List<GameObject> spawnedEntities;
    private const float spawnRate = 1.2f;
    private bool gameIsActive;
    
    [HideInInspector] public static bool paused;
    
    void Start()
    {
        spawnedEntities = new List<GameObject>();
        gameIsActive = true;
        StartCoroutine(SpawnEntities());
    }

    public void DeleteSpawnedEntities()
    {
        foreach (var entity in spawnedEntities)
        {
            Destroy(entity);
        }

        spawnedEntities.Clear();
    }

    // Spawns entities on the right-side spawn points
    private void SpawnLeftMovingEntity()
    {
        if (!gameIsActive) return;
        
        // Select a random right-side spawn point
        var index = Random.Range(0, rightSideSpawnPoints.Count);
        var spawnPoint = rightSideSpawnPoints[index];

        // Select a random entity
        index = Random.Range(0, leftMovingGameObjects.Count);
        var entity = leftMovingGameObjects[index];

        // Spawn the random entity on the right-side of the map
        spawnedEntities.Add(Instantiate(entity, spawnPoint.transform.position, spawnPoint.transform.rotation));
    }

    // Spawn entities on the left-side spawn points
    private void SpawnRightMovingEntity()
    {
        if (!gameIsActive) return;
        
        // Select a random left-side spawn point
        var index = Random.Range(0, leftSideSpawnPoints.Count);
        var spawnPoint = leftSideSpawnPoints[index];

        // Select a random entity
        index = Random.Range(0, rightMovingGameObjects.Count);
        var entity = rightMovingGameObjects[index];

        // Spawn the random entity on the left-side of the map
        spawnedEntities.Add(Instantiate(entity, spawnPoint.transform.position, spawnPoint.transform.rotation));
    }

    // Coroutine to continuously spawn entities while the game is active
    private IEnumerator SpawnEntities()
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
