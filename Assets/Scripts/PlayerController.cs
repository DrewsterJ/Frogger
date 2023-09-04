using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    // Player rigid body
    private Rigidbody2D playerRb;
    private float offset = 0.55f;

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

    private void FixedUpdate()
    {
        RaycastHit hit;
        if (Physics.Raycast(transform.position, transform.TransformDirection(Vector3.forward), out hit, Mathf.Infinity))
        {
            if (hit.collider.CompareTag("Water"))
            {
                var render = GetComponent<SpriteRenderer>();
                render.enabled = false;
                Destroy(this);
            }
            else if (hit.collider.CompareTag("VictorySquare"))
            {
                transform.position = new Vector3(0, -8.23f, -9.33f);
                Debug.Log("Victory!");
            }
            else if (!hit.collider.CompareTag("Untagged"))
            {
                var component = hit.collider.GetComponent<MoveForward>();
                var direction = new Vector2();
                if (component.leftFacing)
                {
                    direction = Vector2.left * (Time.deltaTime * component.speed);
                }
                else
                {
                    direction = Vector2.right * (Time.deltaTime * component.speed);
                }
                
                transform.Translate(direction);
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
    }

    private void OnTriggerExit(Collider other)
    {
    }
}
