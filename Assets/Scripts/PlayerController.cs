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

    private void Start()
    {
        uiScript = uiOverlay.GetComponent<UI>();
    }
    
    void Update()
    {
        HandleInput();
    }
    
    private void FixedUpdate()
    {
        HandleCollisions();
    }

    // Handles user input
    public void HandleInput()
    {
        // Move left
        if (Input.GetKeyDown(KeyCode.A) || Input.GetKeyDown(KeyCode.LeftArrow))
        {
            transform.Translate(Vector2.left + new Vector2(-offset, 0));
        }

        // Move right
        else if (Input.GetKeyDown(KeyCode.D) || Input.GetKeyDown(KeyCode.RightArrow))
        {
            transform.Translate(Vector2.right + new Vector2(offset, 0));
        }

        // Move up
        if (Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.UpArrow))
        {
            transform.Translate(Vector2.up + new Vector2(0, offset));
        }

        // Move down
        else if (Input.GetKeyDown(KeyCode.S) || Input.GetKeyDown(KeyCode.DownArrow))
        {
            transform.Translate(Vector2.down + new Vector2(0, -offset));
        }
    }

    private void HandleWaterCollision()
    {
        uiScript.updateLives();

        if (uiScript.lives <= 0)
        {
            var spriteRenderer = GetComponent<SpriteRenderer>();
            spriteRenderer.enabled = false;
            Destroy(this);
        }
        else
        {
            transform.position = startLocation;
        }
    }

    private void HandleVictorySquareCollision(Collider victorySquare)
    {
        uiScript.updateScore();
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
            else if (!hit.collider.CompareTag("Untagged"))
            {
                HandleFloatingObjectCollision(hit.collider);
            }
        }
    }
}
