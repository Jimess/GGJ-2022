using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class CollisionManager : Singleton<CollisionManager>
{
    private List<GameObject> hitObstacles;

    public int maxCollisions = 3;
    public int collisions;

    public TextMeshProUGUI collisionCountText;

    public delegate void OnMaxCollisions();
    public static OnMaxCollisions onMaxCollisions;

    void Start()
    {
        hitObstacles = new List<GameObject>();
        updateCollisionUI();
    }

    public void countCollision(GameObject obstacle)
    {
        if (!hitObstacles.Contains(obstacle)) {
            hitObstacles.Add(obstacle);

            collisions++;

            updateCollisionUI();

            if (collisions == maxCollisions)
            {
                onMaxCollisions?.Invoke();
                WorldSpinManager.Instance.Spin();
            }
           
        }
    }

    private void updateCollisionUI()
    {
        collisionCountText.SetText(collisions.ToString());
    }
        }
    }

    private void updateCollisionUI()
    {
        collisionCountText.SetText(collisions.ToString());
    }

    public void resetCount()
    {
        hitObstacles = new List<GameObject>();
        collisions = 0;
        updateCollisionUI();
    }
}
