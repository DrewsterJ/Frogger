using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    // Player rigid body
    private Rigidbody2D playerRb;
    private float offset = 0.45f;

    private void Start()
    {
        playerRb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
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

    private void OnCollisionEnter(Collision other)
    {
        Debug.Log("Collided!");
    }
}
