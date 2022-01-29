using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollisionTracker : MonoBehaviour
{
    private CollisionManager collisionManager;

    // Start is called before the first frame update
    void Start()
    {
        collisionManager = FindObjectOfType<CollisionManager>();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        print("collision");
        if (collision.gameObject.tag == "Obstacle")
        {
            collisionManager?.countCollision();
        }
    }
}
