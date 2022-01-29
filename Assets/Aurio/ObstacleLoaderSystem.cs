using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleLoaderSystem : PersistentSingleton<ObstacleLoaderSystem>
{
    private float centerObstacleMargin;
    private float startingHeight;
    private float stepLength;

    public BoxCollider2D gameBounds;

    [Header("Number of times the obstacles are spawned")]
    public int steps = 10;
    [Header("How far away from the edge a center obstacle has to be. % of gameBounds width")]
    public int centerObstacleMarginPercent = 10;
    public int mobSpawnFrequency = 0;

    public List<GameObject> edgeObstacles; //By default, it's a right edge obstacle
    public List<GameObject> centerObstacles;
    public List<GameObject> mobObstacles;

    public void Start()
    {
        centerObstacleMargin = gameBounds.size.x / centerObstacleMarginPercent;

        //Leave 5% of level length start and end empty
        startingHeight = gameBounds.transform.position.y -gameBounds.size.y * 0.05f;
        stepLength = gameBounds.size.y * 0.9f / steps;

        loadObstacles();
    }

    /*public GameObject GetRandomTerrainObstacle() {
        return obstacles[Random.Range(0, obstacles.Count)];
    }*/

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

    private void loadObstacles()
    {
        float obstacleHeight = startingHeight;

        for (int i = 0; i < steps; i++)
        {
            /*Spawning terrain
             * 0 - Spawn right edge
             * 1 - Spawn left edge
             * 2 - spawn center 
             * 3 - spawn spawn both edges
             * 4 - spawn an edge and center
             * 5 - spawn all
            */
            int spawnCase = Random.Range(0, 3);//Kolkas basic

            switch (spawnCase)
            {
                case 0:
                    spawnEdgeObstacle(obstacleHeight, true);
                    break;
                case 1:
                    spawnEdgeObstacle(obstacleHeight, false);
                    break;
                case 2:
                    spawnCenterObstacle(obstacleHeight);
                    break;
                default:
                    break;
            }

            obstacleHeight -= stepLength;
        }

    }

    private void spawnEdgeObstacle(float height, bool rightSide)
    {
        float xPosition = gameBounds.transform.position.x;

        xPosition += rightSide ? gameBounds.size.x / 2 : -gameBounds.size.x / 2;

        Vector3 position = new Vector3(xPosition, height);

        GameObject prefab = GetRandomEdgeObstacle();

        GameObject obj = Instantiate(prefab, position, Quaternion.identity, gameBounds.transform);

        if (!rightSide)
        {
            obj.transform.localScale = new Vector3(-1f, 1f, 1f);
        }
    }

    private void spawnCenterObstacle(float height)
    {
        float maxDistanceFromCenter = (gameBounds.size.x / 2 - centerObstacleMargin);
        float xPosition = Random.Range(gameBounds.transform.position.x - maxDistanceFromCenter, gameBounds.transform.position.x + maxDistanceFromCenter);
        Vector3 position = new Vector3(xPosition, height);

        Quaternion rotation = Quaternion.AngleAxis(Random.Range(0,360), Vector3.forward);

        GameObject prefab = GetRandomCenterObstacle();
        GameObject obj = Instantiate(prefab, position, rotation, gameBounds.transform);
    }
}
