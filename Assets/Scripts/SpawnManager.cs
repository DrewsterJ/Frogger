using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;

public class SpawnManager : MonoBehaviour
{
    public List<GameObject> rightSideSpawnPoints;
    public List<GameObject> leftSideSpawnPoints;
    public List<GameObject> leftMovingGameObjects;
    public List<GameObject> rightMovingGameObjects;
    public List<GameObject> spawnedEntities;
    private const float spawnRate = 1.2f;
    private bool gameIsActive;
    
    [HideInInspector]
    public static bool paused;
    
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

    private void SpawnLeftMovingEntity()
    {
        if (!gameIsActive) return;
        
        var index = Random.Range(0, rightSideSpawnPoints.Count);
        var spawnPoint = rightSideSpawnPoints[index];

        index = Random.Range(0, leftMovingGameObjects.Count);
        var entity = leftMovingGameObjects[index];

        spawnedEntities.Add(Instantiate(entity, spawnPoint.transform.position, spawnPoint.transform.rotation));
    }

    private void SpawnRightMovingEntity()
    {
        if (!gameIsActive) return;
        
        var index = Random.Range(0, leftSideSpawnPoints.Count);
        var spawnPoint = leftSideSpawnPoints[index];

        index = Random.Range(0, rightMovingGameObjects.Count);
        var entity = rightMovingGameObjects[index];

        spawnedEntities.Add(Instantiate(entity, spawnPoint.transform.position, spawnPoint.transform.rotation));
    }

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
