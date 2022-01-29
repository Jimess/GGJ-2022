using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ProgressBarManager : MonoBehaviour
{
    private Slider slider;
    public TextMeshProUGUI meterCount;
    public GameObject startingPoint;
    public GameObject endingPoint;
    public GameObject player;
    float totalDistance;

    private void Awake()
    {
        slider = gameObject.GetComponent<Slider>();
    }

    void Start()
    {
        totalDistance = (startingPoint.transform.position.y - endingPoint.transform.position.y);
    }

    private void FixedUpdate()
    {
        calculateProgressBar();
    }

    private void calculateProgressBar()
    {
        float progress = 1 - ((player.transform.position.y - endingPoint.transform.position.y) / totalDistance);
        if (slider.value < progress || slider.value > progress)
        {
            slider.value = progress;
            if (progress < 0)
            {
                meterCount.text = "Heaven";
            }
            else if (progress > 1)
            {
                meterCount.text = "The end";
            }
            else
            {
                meterCount.text = Mathf.Floor(progress * 1000).ToString() + "m";
            }

        }
    }
}
