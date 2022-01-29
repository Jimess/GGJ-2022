using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProgressBarManager : MonoBehaviour
{
    public GameObject startingPoint;
    public GameObject endingPoint;
    public GameObject player;
    float progress = 0f;
    float totalDistance;

    // Start is called before the first frame update
    void Start()
    {
        totalDistance = Vector2.Distance(startingPoint.GetComponent<Rigidbody2D>().transform.position, endingPoint.GetComponent<Rigidbody2D>().transform.position);
    }

    private void FixedUpdate()
    {
        progress = 1 - Vector2.Distance(player.GetComponent<Rigidbody2D>().transform.position, endingPoint.GetComponent<Rigidbody2D>().transform.position) / totalDistance;
        print(progress);
    }
}
