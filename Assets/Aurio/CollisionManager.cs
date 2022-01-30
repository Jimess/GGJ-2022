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

    [SerializeField] List<GameObject> normalImgs;
    [SerializeField] List<GameObject> heavenImgs;
    [SerializeField] List<GameObject> theEndImgs;

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
        hitObstacles.Add(mob);

        if (collisions == maxCollisions)
        {
            onMaxCollisions?.Invoke();
            WorldSpinManager.Instance.Spin();
        }
    }

    private void updateCollisionUI()
    {
        collisionCountText.SetText(collisions.ToString());

        for (int i = 0; i < normalImgs.Count; i++) {
            normalImgs[i].SetActive(false);
            heavenImgs[i].SetActive(false);
            theEndImgs[i].SetActive(false);
            if (collisions > i) {
                if (WorldSpinManager.Instance.isGoingToEnd()) {
                    heavenImgs[i].SetActive(true);
                } else {
                    theEndImgs[i].SetActive(true);
                }
            } else {
                normalImgs[i].SetActive(true);
            }
        }
    }

    public void restartCollisionCount()
    {
        GameObject lastObstacle = hitObstacles[hitObstacles.Count - 1];
        hitObstacles.Clear();
        hitObstacles.Add(lastObstacle);
        collisions = 0;
        updateCollisionUI();
    }
}
