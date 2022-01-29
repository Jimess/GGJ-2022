using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleLoaderSystem : Singleton<ObstacleLoaderSystem>
{
    public List<GameObject> edgeObstacles; //By default, it's a right edge obstacle
    public List<GameObject> centerObstacles;
    public GameObject angelMob;
    public GameObject devilMob;

    public GameObject GetAngelMob()
    {
        return angelMob;
    }

    public GameObject GetDevilMob()
    {
        return devilMob;
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
