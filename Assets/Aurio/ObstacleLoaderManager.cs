using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleLoaderManager : MonoBehaviour
{
    public BoxCollider2D gameBounds;
    public bool isAngelMob = true;

    private ObstacleLoaderSystem obstacleLoaderSystem;

    private string obstacleTag = "Obstacle";
    private float ObstacleMarginHeightPercent = 5f;
    float marginHeight;

    private float centerObstacleMargin;
    private float startingHeight;
    private float stepLength;

    [Header("Number of times the obstacles are spawned")]
    public int steps = 10;
    [Header("How far away from the edge a center obstacle has to be")]
    public int centerObstacleMarginPercent = 10;
    public int mobCount = 2;

    // Start is called before the first frame update
    void Start()
    {
        obstacleLoaderSystem = FindObjectOfType<ObstacleLoaderSystem>();

        centerObstacleMargin = gameBounds.size.x * centerObstacleMarginPercent / 100;

        marginHeight = gameBounds.size.y * ObstacleMarginHeightPercent / 100;

        startingHeight = gameBounds.transform.position.y + gameBounds.size.y / 2 - marginHeight;
        stepLength = (gameBounds.size.y - marginHeight * 2) / steps;

        loadObstacles();
    }

    private void loadObstacles()
    {
        float obstacleHeight = startingHeight;

        List<int> mobSpawnStep = new();

        for (int i = 0; i < mobCount; i++)
        {
            int step = Random.Range(0, steps);
            mobSpawnStep.Add(step);
        }
        mobSpawnStep.Sort();

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
            int spawnCase = Random.Range(0, 6);

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
                case 3:
                    spawnEdgeObstacle(obstacleHeight, false);
                    spawnEdgeObstacle(obstacleHeight, true);
                    break;
                case 4:
                    spawnEdgeObstacle(obstacleHeight, Random.Range(0,2) == 1);
                    spawnCenterObstacle(obstacleHeight);
                    break;
                case 5:
                    spawnEdgeObstacle(obstacleHeight, false);
                    spawnEdgeObstacle(obstacleHeight, true);
                    spawnCenterObstacle(obstacleHeight);
                    break;
                default:
                    break;
            }

            while (mobSpawnStep.Count > 0 && mobSpawnStep[0] == i)
            {
                spawnMob(obstacleHeight);
                mobSpawnStep.RemoveAt(0);
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

        obj.transform.GetChild(0).gameObject.tag = obstacleTag;
    }

    private void spawnCenterObstacle(float height)
    {
        float maxDistanceFromCenter = (gameBounds.size.x / 2 - centerObstacleMargin);
        float xPosition = Random.Range(gameBounds.transform.position.x - maxDistanceFromCenter, gameBounds.transform.position.x + maxDistanceFromCenter);
        Vector3 position = new Vector3(xPosition, height);

        Quaternion rotation = Quaternion.AngleAxis(Random.Range(0, 360), Vector3.forward);

        GameObject prefab = obstacleLoaderSystem.GetRandomCenterObstacle();
        GameObject obj = Instantiate(prefab, position, rotation, gameBounds.transform);

        obj.transform.GetChild(0).gameObject.tag = obstacleTag;
    }

    private void spawnMob(float height)
    {
        float maxDistanceFromCenter = (gameBounds.size.x / 2 - centerObstacleMargin);
        float xPosition = Random.Range(gameBounds.transform.position.x - maxDistanceFromCenter, gameBounds.transform.position.x + maxDistanceFromCenter);
        Vector3 position = new Vector3(xPosition, height);

        GameObject prefab;

        if (isAngelMob)
            prefab = obstacleLoaderSystem.GetAngelMob();
        else
            prefab = obstacleLoaderSystem.GetDevilMob();

        GameObject obj = Instantiate(prefab, position, Quaternion.identity, gameBounds.transform);
    }

    public void transformMobs()
    {
        //Transform angels to devils and vise versa
    }

    private void OnDrawGizmosSelected() {
        float newCenterObstacleMargin = gameBounds.size.x * centerObstacleMarginPercent / 100;
        float newMarginHeight = gameBounds.size.y * ObstacleMarginHeightPercent / 100;

        Gizmos.color = Color.red;
        Gizmos.DrawWireCube(gameBounds.transform.position, new Vector3(gameBounds.size.x - newCenterObstacleMargin, gameBounds.size.y - newMarginHeight * 2, 0));
    }
}
