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
    
    public List<GameObject> victorySquares;
    
    [HideInInspector] public static bool paused;
    public static bool gameStarted;
    [HideInInspector] public static bool gameWon;
    [HideInInspector] public static bool gameLost;
    
    // Start is called before the first frame update
    void Start()
    {
        uiScript = uiOverlay.GetComponent<UI>();
        audioControlScript = camera.GetComponent<AudioControl>();
        spawnManagerScript = spawnManager.GetComponent<SpawnManager>();
        playerControllerScript = player.GetComponent<PlayerController>();
        gameStarted = false;
        
        paused = true;
        MoveForward.paused = true;
        SpawnManager.paused = true;
        PlayerController.paused = true;
        
        uiScript.mainMenu.visible = true;
        uiScript.mainMenuLabel.visible = true;
        audioControlScript.mainMenuMusic.Play();
        gameWon = false;
        gameLost = false;
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
        audioControlScript.diedMusic.Stop();
        uiScript.lossMenuLabel.visible = false;
        gameWon = false;
        gameLost = false;
        gameStarted = true;
        
        // Resets victory squares
        foreach (var square in victorySquares)
        {
            var victorySquareScript = square.GetComponent<VictorySquare>();
            victorySquareScript.active = true;
            var spriteRenderer = square.GetComponent<SpriteRenderer>();
            spriteRenderer.color = Color.white;
        }
    }

    public void RestartGame()
    {
        StopGame();
        StartGame();
    }

    public void WinGame()
    {
        gameWon = true;
        paused = true;
        MoveForward.paused = true;
        SpawnManager.paused = true;
        PlayerController.paused = true;
        audioControlScript.mainMenuMusic.Stop();
        audioControlScript.gameplayMusic[audioControlScript.activeSongIndex].Stop();
        uiScript.victoryMenu.visible = true;
        uiScript.victoryMenuLabel.visible = true;
    }

    public void LostGame()
    {
        gameLost = true;
        paused = true;
        MoveForward.paused = true;
        SpawnManager.paused = true;
        PlayerController.paused = true;
        audioControlScript.mainMenuMusic.Stop();
        audioControlScript.gameplayMusic[audioControlScript.activeSongIndex].Stop();
        uiScript.victoryMenu.visible = true;
        uiScript.lossMenuLabel.visible = true;
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
        uiScript.victoryMenu.visible = false;
        uiScript.victoryMenuLabel.visible = false;
        audioControlScript.wonMusic.Stop();
        audioControlScript.gameplayMusic[audioControlScript.activeSongIndex].Stop();
        playerControllerScript.MoveBackToStart();
        spawnManagerScript.DeleteSpawnedEntities();
        uiScript.lives = 3;
        uiScript.score = 0;
        uiScript.victoryMenuLabel.visible = false;
        uiScript.victoryMenu.visible = false;
        uiScript.lossMenuLabel.visible = false;
        audioControlScript.diedMusic.Stop();
        gameStarted = false;
        
        // Resets victory squares
        foreach (var square in victorySquares)
        {
            square.active = true;
            var spriteRenderer = square.GetComponent<SpriteRenderer>();
            spriteRenderer.color = Color.white;
        }

        // Resets player lives
        foreach (var heartImage in uiScript.hearts)
        {
            heartImage.SetEnabled(true);
        }
    }

    // Used to hide/show the settings menu
    public void SwitchSettings()
    {
        uiScript.settingsMenu.visible = !uiScript.settingsMenu.visible;
    }

    // Used to pause/resume the game
    public void SwitchPause()
    {
        paused = !paused;
        MoveForward.paused = paused;
        SpawnManager.paused = paused;
        PlayerController.paused = paused;
        uiScript.pauseMenuLabel.visible = paused;
        uiScript.pauseMenu.visible = paused;
        uiScript.settingsMenu.visible = false;

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
        // When the player presses "escape" or "p", the game will pause/unpause
        if ((Input.GetKeyDown(KeyCode.P) || Input.GetKeyDown(KeyCode.Escape)) && !gameWon && !gameLost && gameStarted)
        {
            SwitchPause();
        }
    }
}
