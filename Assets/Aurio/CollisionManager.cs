using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class CollisionManager : Singleton<CollisionManager>
{
    public int maxCollisions = 3;
    public int collisions;

    public TextMeshProUGUI collisionCountText;

    public delegate void OnMaxCollisions();
    public static OnMaxCollisions onMaxCollisions;

    void Start()
    {
        updateCollisionUI();
    }

    public void countCollision()
    {
        collisions++;

        updateCollisionUI();

        if (collisions >= maxCollisions)
        {
            onMaxCollisions?.Invoke();
        }
    }

    private void updateCollisionUI()
    {
        collisionCountText.SetText(collisions.ToString());
    }
}
