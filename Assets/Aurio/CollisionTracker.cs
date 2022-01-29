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

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Obstacle")
        {
            collisionManager.countCollision();
        }
    }
}
