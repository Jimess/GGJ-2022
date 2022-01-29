using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleLoaderSystem : Singleton<ObstacleLoaderSystem>
{
    public List<GameObject> edgeObstacles; //By default, it's a right edge obstacle
    public List<GameObject> centerObstacles;
    public List<GameObject> mobObstacles;

    public GameObject GetRandomMobObstacle()
    {
        return mobObstacles[Random.Range(0, mobObstacles.Count)];
    }

    public GameObject GetRandomCenterObstacle()
    {
        return centerObstacles[Random.Range(0, centerObstacles.Count)];
    }
    public GameObject GetRandomEdgeObstacle()
    {
        return edgeObstacles[Random.Range(0, edgeObstacles.Count)];
    }
}
