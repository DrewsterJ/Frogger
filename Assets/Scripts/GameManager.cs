using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public GameObject uiOverlay;
    private UI uiScript;

    public GameObject camera;
    private AudioControl audioControlScript;

    public GameObject spawnManager;
    private SpawnManager spawnManagerScript;

    public GameObject player;
    private PlayerController playerControllerScript;
    
    [HideInInspector]
    public static bool paused;
    
    // Start is called before the first frame update
    void Start()
    {
        uiScript = uiOverlay.GetComponent<UI>();
        audioControlScript = camera.GetComponent<AudioControl>();
        spawnManagerScript = spawnManager.GetComponent<SpawnManager>();
        playerControllerScript = player.GetComponent<PlayerController>();
        
        paused = true;
        MoveForward.paused = true;
        SpawnManager.paused = true;
        PlayerController.paused = true;
        
        uiScript.mainMenu.visible = true;
        uiScript.mainMenuLabel.visible = true;
        audioControlScript.mainMenuMusic.Play();
    }

    public void StartGame()
    {
        paused = false;
        MoveForward.paused = false;
        SpawnManager.paused = false;
        PlayerController.paused = false;
        uiScript.mainMenu.visible = false;
        uiScript.mainMenuLabel.visible = false;
        audioControlScript.mainMenuMusic.Stop();
        audioControlScript.gameplayMusic[audioControlScript.activeSongIndex].Play();
    }

    public void StopGame()
    {
        paused = true;
        MoveForward.paused = true;
        SpawnManager.paused = true;
        PlayerController.paused = true;
        uiScript.mainMenu.visible = true;
        uiScript.mainMenuLabel.visible = true;
        uiScript.pauseMenu.visible = false;
        uiScript.pauseMenuLabel.visible = false;
        audioControlScript.mainMenuMusic.Play();
        audioControlScript.pauseMenuMusic.Stop();
        audioControlScript.gameplayMusic[audioControlScript.activeSongIndex].Stop();
        playerControllerScript.MoveBackToStart();
        spawnManagerScript.DeleteSpawnedEntities();
        uiScript.lives = 3;
        uiScript.score = 0;

        foreach (var heartImage in uiScript.hearts)
        {
            heartImage.SetEnabled(true);
        }
    }

    public void SwitchPause()
    {
        paused = !paused;
        MoveForward.paused = paused;
        SpawnManager.paused = paused;
        PlayerController.paused = paused;
        uiScript.pauseMenuLabel.visible = paused;
        uiScript.pauseMenu.visible = paused;

        if (paused)
        {
            audioControlScript.pauseMenuMusic.Play();
            audioControlScript.gameplayMusic[audioControlScript.activeSongIndex].Pause();
        }
        else
        {
            audioControlScript.pauseMenuMusic.Stop();
            audioControlScript.gameplayMusic[audioControlScript.activeSongIndex].Play();
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.P) || Input.GetKeyDown(KeyCode.Escape))
        {
            SwitchPause();
        }
    }
}
