using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public MoveForward moveForwardScript;
    public SpawnManager spawnManagerScript;
    public PlayerController playerControllerScript;
    
    [HideInInspector]
    public static bool paused;
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.P) || Input.GetKeyDown(KeyCode.Escape))
        {
            paused = !paused;
            MoveForward.paused = paused;
            SpawnManager.paused = paused;
            PlayerController.paused = paused;
        }
    }
}
