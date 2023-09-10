using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    // Offset for moving the character object
    private const float offset = 0.55f;
    
    // Game UI overlay objects
    public GameObject uiOverlay;
    private UI uiScript;
    
    // Start location
    private readonly Vector3 startLocation = new Vector3(0, -8.23f, -9.33f);
    
    [HideInInspector]
    public static bool paused;

    public GameObject camera;
    private AudioControl audioControlScript;

    private void Start()
    {
        uiScript = uiOverlay.GetComponent<UI>();
        audioControlScript = camera.GetComponent<AudioControl>();
    }
    
    void Update()
    {
        if (paused) return;
        
        HandleInput();
    }
    
    private void FixedUpdate()
    {
        if (paused) return;
        
        HandleCollisions();
    }

    // Handles user input
    public void HandleInput()
    {
        // Move left
        if (Input.GetKeyDown(KeyCode.A) || Input.GetKeyDown(KeyCode.LeftArrow))
        {
            transform.Translate(Vector2.left + new Vector2(-offset, 0));
            PlaySounds();
        }

        // Move right
        else if (Input.GetKeyDown(KeyCode.D) || Input.GetKeyDown(KeyCode.RightArrow))
        {
            transform.Translate(Vector2.right + new Vector2(offset, 0));
            PlaySounds();
        }

        // Move up
        if (Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.UpArrow))
        {
            transform.Translate(Vector2.up + new Vector2(0, offset));
            PlaySounds();
        }

        // Move down
        else if (Input.GetKeyDown(KeyCode.S) || Input.GetKeyDown(KeyCode.DownArrow))
        {
            transform.Translate(Vector2.down + new Vector2(0, -offset));
            PlaySounds();
        }
    }

    public void MoveBackToStart()
    {
        transform.position = startLocation;
    }

    private void HandleWaterCollision()
    {
        uiScript.UpdateLives();
        
        if (uiScript.lives <= 0)
        {
            audioControlScript.diedAudio.Play();
            var spriteRenderer = GetComponent<SpriteRenderer>();
            spriteRenderer.enabled = false;
            audioControlScript.gameplayMusic[audioControlScript.activeSongIndex].Stop();
            audioControlScript.diedMusic.Play();
            Destroy(this);
        }
        else
        {
            audioControlScript.waterHurtAudio.Play();
            transform.position = startLocation;
        }
    }

    private void HandleVictorySquareCollision(Collider victorySquare)
    {
        uiScript.UpdateScore();

        if (uiScript.score == 3)
        {
            audioControlScript.wonMusic.Play();
        }
        audioControlScript.scoreAudio.Play();
        transform.position = startLocation;
    }

    private void HandleFloatingObjectCollision(Collider floatingObject)
    {
        var floatingObjectScript = floatingObject.GetComponent<MoveForward>();
        if (floatingObjectScript.leftMoving)
        {
            transform.Translate(Vector2.left * (Time.deltaTime * floatingObjectScript.speed));
        }
        else
        {
            transform.Translate(Vector2.right * (Time.deltaTime * floatingObjectScript.speed));
        }
    }

    // Plays sounds based on player movement
    private void PlaySounds()
    {
        if (Physics.Raycast(transform.position, transform.TransformDirection(Vector3.forward), out RaycastHit hit,
                Mathf.Infinity))
        {
            if (hit.collider.CompareTag("Water"))
            {
                audioControlScript.waterHurtAudio.Play();
            }
            else if (hit.collider.CompareTag("VictorySquare"))
            {
                audioControlScript.scoreAudio.Play();
            }
            else if (hit.collider.CompareTag("Turtle") || hit.collider.CompareTag("Crocodile") ||
                     hit.collider.CompareTag("Log"))
            {
                audioControlScript.otherMovementAudio.Play();
            }
            else
            {
                audioControlScript.grassMovementAudio.Play();
            }
        }
    }

    // Controls game logic when player object collides with other game objects
    private void HandleCollisions()
    {
        if (Physics.Raycast(transform.position, transform.TransformDirection(Vector3.forward), out RaycastHit hit, Mathf.Infinity))
        {
            if (hit.collider.CompareTag("Water"))
            {
                HandleWaterCollision();
            }
            else if (hit.collider.CompareTag("VictorySquare"))
            {
                HandleVictorySquareCollision(hit.collider);
            }
            else if (hit.collider.CompareTag("Turtle") || hit.collider.CompareTag("Crocodile") || hit.collider.CompareTag("Log"))
            {
                HandleFloatingObjectCollision(hit.collider);
            }
        }
    }
}
