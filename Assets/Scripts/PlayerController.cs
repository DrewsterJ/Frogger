using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    // Offset for moving the character object
    private const float offset = 0.8f;
    
    // Game UI overlay objects
    public GameObject uiOverlay;
    private UI uiScript;
    
    // Start location
    private readonly Vector3 startLocation = new Vector3(0, -8.23f, -9.33f);
    
    [HideInInspector]
    public static bool paused;

    public GameObject camera;
    private AudioControl audioControlScript;
    public GameObject gameManager;
    private GameManager gameManagerScript;

    public Sprite frogLeftSprite;
    public Sprite frogRightSprite;
    public Sprite frogForwardSprite;
    public Sprite frogBackwardSprite;

    private void Start()
    {
        uiScript = uiOverlay.GetComponent<UI>();
        audioControlScript = camera.GetComponent<AudioControl>();
        gameManagerScript = gameManager.GetComponent<GameManager>();
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
            var spriteRenderer = GetComponent<SpriteRenderer>();
            if (spriteRenderer.sprite != frogLeftSprite)
            {
                spriteRenderer.sprite = frogLeftSprite;
            }
            transform.Translate(Vector2.left + new Vector2(-offset, 0));
            PlaySounds();
        }

        // Move right
        else if (Input.GetKeyDown(KeyCode.D) || Input.GetKeyDown(KeyCode.RightArrow))
        {
            var spriteRenderer = GetComponent<SpriteRenderer>();

            if (spriteRenderer.sprite != frogRightSprite)
            {
                spriteRenderer.sprite = frogRightSprite;
            }
            transform.Translate(Vector2.right + new Vector2(offset, 0));
            PlaySounds();
        }

        // Move up
        if (Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.UpArrow))
        {
            var spriteRenderer = GetComponent<SpriteRenderer>();

            if (spriteRenderer.sprite != frogForwardSprite)
            {
                spriteRenderer.sprite = frogForwardSprite;
            }
            transform.Translate(Vector2.up + new Vector2(0, offset));
            PlaySounds();
        }

        // Move down
        else if (Input.GetKeyDown(KeyCode.S) || Input.GetKeyDown(KeyCode.DownArrow))
        {
            var spriteRenderer = GetComponent<SpriteRenderer>();

            if (spriteRenderer.sprite != frogBackwardSprite)
            {
                spriteRenderer.sprite = frogBackwardSprite;
            }
            transform.Translate(Vector2.down + new Vector2(0, -offset));
            PlaySounds();
        }
    }

    public void MoveBackToStart()
    {
        transform.position = startLocation;
        GetComponent<SpriteRenderer>().sprite = frogForwardSprite;
    }

    private void HandleWaterCollision()
    {
        uiScript.UpdateLives();
        
        if (uiScript.lives <= 0)
        {
            audioControlScript.diedAudio.Play();
            audioControlScript.gameplayMusic[audioControlScript.activeSongIndex].Stop();
            audioControlScript.diedMusic.Play();
            gameManagerScript.LostGame();
        }
        else
        {
            audioControlScript.waterHurtAudio.Play();
            transform.position = startLocation;
        }
    }

    private void HandleVictorySquareCollision(Collider victorySquare)
    {
        var victorySquareScript = victorySquare.GetComponent<VictorySquare>();

        if (!victorySquareScript.active)
        {
            return;
        }

        var victorySquareSprite = victorySquare.GetComponent<SpriteRenderer>();
        victorySquareSprite.color = Color.magenta;
        
        // Disable the victory square
        victorySquareScript.active = false;
        
        uiScript.UpdateScore();
        
        if (uiScript.score == 5)
        {
            gameManagerScript.WinGame();
            audioControlScript.wonMusic.Play();
            audioControlScript.scoreAudio.Play();
        }
        else
        {
            audioControlScript.scoreAudio.Play();
            transform.position = startLocation;
        }
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
                var victorySquareScript = hit.collider.GetComponent<VictorySquare>();
                if (victorySquareScript.active)
                {
                    audioControlScript.scoreAudio.Play();
                }
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
            else if (hit.collider.CompareTag("WaterBoundary"))
            {
                HandleWaterCollision();
            }
            else if (hit.collider.CompareTag("LeftSideBoundary"))
            {
                transform.Translate(Vector2.right + new Vector2(offset, 0));
            }
            else if (hit.collider.CompareTag("RightSideBoundary"))
            {
                transform.Translate(Vector2.left + new Vector2(-offset, 0));
            }
            else if (hit.collider.CompareTag("TopBoundary"))
            {
                transform.Translate(Vector2.down + new Vector2(0, -offset));
            }
            else if (hit.collider.CompareTag("BottomBoundary"))
            {
                transform.Translate(Vector2.up + new Vector2(0, offset));
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
