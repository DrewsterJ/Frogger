using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveForward : MonoBehaviour
{
    public float speed = 5.0f;
    public bool leftFacing = false;
    
    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        if (leftFacing)
        {
            transform.Translate(Vector2.left * (Time.deltaTime * speed));
        }
        else
        {
            transform.Translate(Vector2.right * (Time.deltaTime * speed));
        }
        
    }
}
