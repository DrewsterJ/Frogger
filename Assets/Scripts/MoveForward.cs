using System.Collections;
using UnityEngine;

public class MoveForward : MonoBehaviour
{
    public float speed = 5.0f;
    public bool leftMoving;
    [HideInInspector] public static bool paused;

    void Start()
    {
        StartCoroutine(AutoDestroy());
    }

    // Constantly moves the entity to the left or right
    void Update()
    {
        if (paused) return;
        
        if (leftMoving)
        {
            transform.Translate(Vector2.left * (Time.deltaTime * speed));
        }
        else
        {
            transform.Translate(Vector2.right * (Time.deltaTime * speed));
        }
    }

    // Automatically destroys the game object after 13 seconds (when it leaves the visible camera area)
    private IEnumerator AutoDestroy()
    {
        yield return new WaitForSeconds(13);
        Destroy(gameObject);
    }
}
