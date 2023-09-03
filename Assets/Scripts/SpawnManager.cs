using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    public List<GameObject> leftMovingSpawnPoints;
    public List<GameObject> rightMovingSpawnPoints;
    public List<GameObject> leftMovingGameObjects;
    public List<GameObject> rightMovingGameObjects;
    private float spawnRate = 1.5f;
    private bool gameIsActive = false;
    
    // Start is called before the first frame update
    void Start()
    {
        gameIsActive = true;
        StartCoroutine(spawnEntities());
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    IEnumerator spawnEntities()
    {
        while (gameIsActive)
        {
            yield return new WaitForSeconds(spawnRate);
            bool leftFacingSpawn = (Random.Range(0, 2) == 0);

            if (gameIsActive)
            {
                if (leftFacingSpawn)
                {
                    int spawnIndex = Random.Range(0, leftMovingSpawnPoints.Count);
                    int entityIndex = Random.Range(0, leftMovingGameObjects.Count);
                    Instantiate(leftMovingGameObjects[entityIndex], leftMovingSpawnPoints[spawnIndex].transform.position,
                        leftMovingSpawnPoints[spawnIndex].transform.rotation);
                }
                else
                {
                    int spawnIndex = Random.Range(0, rightMovingSpawnPoints.Count);
                    int entityIndex = Random.Range(0, rightMovingGameObjects.Count);
                    Instantiate(rightMovingGameObjects[entityIndex],
                        rightMovingSpawnPoints[spawnIndex].transform.position,
                        rightMovingSpawnPoints[spawnIndex].transform.rotation);
                }
            }
        }
    }
}
