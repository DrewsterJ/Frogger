using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public GameObject uiOverlay;
    private UI uiScript;

    public GameObject camera;
    private AudioControl audioControlScript;
    
    [HideInInspector]
    public static bool paused;
    
    // Start is called before the first frame update
    void Start()
    {
        uiScript = uiOverlay.GetComponent<UI>();
        audioControlScript = camera.GetComponent<AudioControl>();
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
            audioControlScript.gameplayMusic[audioControlScript.activeSongIndex].Stop();
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
