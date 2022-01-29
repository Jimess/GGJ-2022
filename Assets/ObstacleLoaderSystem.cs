using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleLoaderSystem : PersistentSingleton<ObstacleLoaderSystem>
{
    public List<GameObject> obstacles;

    public GameObject GetRandomObstacleSmall() {
        return obstacles[Random.Range(0, obstacles.Count)];
    }
}
