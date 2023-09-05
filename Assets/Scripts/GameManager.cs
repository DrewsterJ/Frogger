using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public GameObject uiOverlay;
    private UI uiScript;
    
    [HideInInspector]
    public static bool paused;
    
    // Start is called before the first frame update
    void Start()
    {
        uiScript = uiOverlay.GetComponent<UI>();
    }

    public void SwitchPause()
    {
        paused = !paused;
        MoveForward.paused = paused;
        SpawnManager.paused = paused;
        PlayerController.paused = paused;
        uiScript.pauseMenuLabel.visible = paused;
        uiScript.pauseMenu.visible = paused;
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
