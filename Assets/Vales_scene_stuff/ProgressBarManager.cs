using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ProgressBarManager : MonoBehaviour
{
    private Slider slider;
    public GameObject startingPoint;
    public GameObject endingPoint;
    public GameObject player;
    float totalDistance;

    private void Awake()
    {
        slider = gameObject.GetComponent<Slider>();
    }

    // Start is called before the first frame update
    void Start()
    {
        totalDistance = Vector2.Distance(startingPoint.transform.position, endingPoint.GetComponent<Rigidbody2D>().transform.position);
    }

    private void FixedUpdate()
    {
        float progress = 1 - Vector2.Distance(player.transform.position, endingPoint.transform.position) / totalDistance;
        if (slider.value < progress)
        {
            slider.value = progress;
        } else if (slider.value > progress)
        {
            slider.value = progress;
        }
    }
}
