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

    //CONSTS
    private const float SCREEN_WIDTH = 17.77778f;

    //[SerializeField] private GameObject gameBounds;
    [Header("Refs")]
    [SerializeField] private List<GameObject> walls;
    [SerializeField] private Transform playerStart;
    [SerializeField] Transform playerEnd;
    [SerializeField] GameObject gatesOfHeaven; //need to place on top of map

    [Header("Level SETTINGS")]
    public float levelHeight = 200;
    public float playerStartOffsetY = 40f;
    public float playerEndOffsetY = 40f; // will be subtracted to move down
    public float gatesOfHeavenOffsetY = 2f;


    // Start is called before the first frame update
    void Start()
    {
        obstacleLoaderSystem = FindObjectOfType<ObstacleLoaderSystem>();

        centerObstacleMargin = gameBounds.size.x * centerObstacleMarginPercent / 100;

        //marginHeight = gameBounds.size.y + gameBounds.offset.y * ObstacleMarginHeightPercent / 100; // manau kad nereikia

        //rasti virsutines bound dalies starta
        //startingHeight = gameBounds.transform.position.y + gameBounds.size.y / 2 - marginHeight;
        startingHeight = gameBounds.transform.position.y + gameBounds.size.y / 2 + gameBounds.offset.y;
        stepLength = (gameBounds.size.y - marginHeight * 2) / steps;

        loadObstacles();
    }

    private void loadObstacles()
    {
        float obstacleHeight = startingHeight;

        List<int> mobSpawnStep = new List<int>();

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
        //float newMarginHeight = gameBounds.size.y + gameBounds.offset.y * ObstacleMarginHeightPercent / 100;
        float newMarginHeight = gameBounds.size.y;//gameBounds.transform.position.y - gameBounds.size.y / 2 + gameBounds.offset.y; //* ObstacleMarginHeightPercent / 100;

        Gizmos.color = Color.red;
        Gizmos.DrawWireCube(new Vector3(gameBounds.transform.position.x, gameBounds.transform.position.y + gameBounds.offset.y), new Vector3(gameBounds.size.x - newCenterObstacleMargin, newMarginHeight, 0));
    }

    //Kvieciamas kiekviena kart kai keiciasi inspector values
    private void OnValidate() {
        //paupdeitint gamebounds, game walls ir player end ir start pozicijas
        //gameBounds.transform.position = new Vector3(0, -levelHeight / 2, 0);
        //BoxCollider2D boundsCol = GetComponent<BoxCollider2D>();
        gameBounds.size = new Vector2(SCREEN_WIDTH, levelHeight);
        gameBounds.offset = new Vector2(0, -levelHeight / 2); // do not spawn obstacles in end game area

        foreach (GameObject obj in walls) {
            BoxCollider2D wallBoxCol = obj.GetComponent<BoxCollider2D>();
            wallBoxCol.size = new Vector2(SCREEN_WIDTH, levelHeight + playerEndOffsetY + playerStartOffsetY);
            //wallBoxCol.offset = new Vector2(0, -levelHeight / 2);

            obj.transform.position = new Vector3(obj.transform.position.x, -levelHeight / 2 - playerEndOffsetY/2 + playerStartOffsetY/2);

            obj.GetComponent<SpriteRenderer>().size = new Vector2(SCREEN_WIDTH, levelHeight + playerEndOffsetY + playerStartOffsetY);
        }

        playerStart.transform.position = new Vector3(0, gameBounds.transform.position.y + playerStartOffsetY);
        playerEnd.transform.position = new Vector3(0, gameBounds.transform.position.y - gameBounds.size.y - playerEndOffsetY);

        gatesOfHeaven.transform.position = new Vector3(gatesOfHeaven.transform.position.x, playerStart.transform.position.y - gatesOfHeavenOffsetY);
    }
}
