using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveForward : MonoBehaviour
{
    public float speed = 5.0f;
    public bool leftMoving;

    // Update is called once per frame
    void Update()
    {
        if (leftMoving)
        {
            transform.Translate(Vector2.left * (Time.deltaTime * speed));
        }
        else
        {
            transform.Translate(Vector2.right * (Time.deltaTime * speed));
        }
    }
}
