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
        totalDistance = (startingPoint.transform.position.y - endingPoint.transform.position.y);
    }

    private void FixedUpdate()
    {
        float progress = 1 - ((player.transform.position.y - endingPoint.transform.position.y) / totalDistance);
        print(progress);
        if (slider.value < progress)
        {
            slider.value = progress;
        } else if (slider.value > progress)
        {
            slider.value = progress;
        }
    }
}
