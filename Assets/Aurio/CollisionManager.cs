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
    public delegate void OnCollision();
    public static OnCollision onCollision;

    void Start()
    {
        hitObstacles = new List<GameObject>();
        updateCollisionUI();
    }

    public void countCollision(GameObject obstacle)
    {
        if (obstacle.CompareTag("HeavenDoor") && !WorldSpinManager.Instance.isGoingToEnd()) {
            WorldSpinManager.Instance.Spin();
            restartCollisionCount();
            updateCollisionUI();
            onCollision?.Invoke();
            return;
        }

        if (obstacle.CompareTag("Mob")) {
            countMobCollision(obstacle);
            return;
        }

        if (!hitObstacles.Contains(obstacle)) {
            hitObstacles.Add(obstacle);

            DialogManager.Instance.ShowDialog(1f, "Ouch....");

            collisions++;
            updateCollisionUI();
            onCollision?.Invoke();

            if (collisions == maxCollisions)
            {
                onMaxCollisions?.Invoke();
                WorldSpinManager.Instance.Spin();
            }
        }
    }

    private void countMobCollision(GameObject mob)
    {
        collisions++;
        updateCollisionUI();

        if (collisions == maxCollisions)
        {
            onMaxCollisions?.Invoke();
            WorldSpinManager.Instance.Spin();
        }
    }

    private void updateCollisionUI()
    {
        collisionCountText.SetText(collisions.ToString());
    }

    public void restartCollisionCount()
    {
        hitObstacles.Clear();
        collisions = 0;
        updateCollisionUI();
    }
}
