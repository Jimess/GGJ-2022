using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleLoaderManager : MonoBehaviour
{
    public BoxCollider2D gameBounds;

    private ObstacleLoaderSystem obstacleLoaderSystem;

    private float centerObstacleMargin;
    private float startingHeight;
    private float stepLength;

    [Header("Number of times the obstacles are spawned")]
    public int steps = 10;
    [Header("How far away from the edge a center obstacle has to be. % of gameBounds width")]
    public int centerObstacleMarginPercent = 10;
    public int mobSpawnFrequency = 0;

    // Start is called before the first frame update
    void Start()
    {
        obstacleLoaderSystem = FindObjectOfType<ObstacleLoaderSystem>();

        centerObstacleMargin = gameBounds.size.x / centerObstacleMarginPercent;

        //Leave 5% of level length start and end empty
        startingHeight = gameBounds.transform.position.y - gameBounds.size.y * 0.05f;
        stepLength = gameBounds.size.y * 0.9f / steps;

        loadObstacles();
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

        GameObject prefab = obstacleLoaderSystem.GetRandomEdgeObstacle();

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

        Quaternion rotation = Quaternion.AngleAxis(Random.Range(0, 360), Vector3.forward);

        GameObject prefab = obstacleLoaderSystem.GetRandomCenterObstacle();
        GameObject obj = Instantiate(prefab, position, rotation, gameBounds.transform);
    }
}
