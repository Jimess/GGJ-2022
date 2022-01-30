using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using DG.Tweening;

public class ProgressBarManager : MonoBehaviour
{
    private Slider slider;
    public TextMeshProUGUI meterCount;
    public GameObject startingPoint;
    public GameObject endingPoint;
    public GameObject player;
    float totalDistance;

    private bool isHeavenEnabled = false;

    [Header("Refs")]
    public RectTransform theEndPanel;
    public RectTransform heavenPanel;

    private void Awake()
    {
        slider = gameObject.GetComponent<Slider>();
    }

    void Start()
    {
        //heaven panel is disabled until you reach the middle, to confuse the player;
        heavenPanel.localScale = Vector2.zero;

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
            meterCount.text = Mathf.Floor(progress * 1000).ToString() + "m";

            if (progress > 0.5f && !isHeavenEnabled) {
                isHeavenEnabled = true;
                heavenPanel.DOScale(Vector3.one, 0.5f).SetEase(Ease.OutElastic);
                LevelManager.Instance.OpenGatesOfHeaven();
            }

            //manau reikia rodyti heaven ir hell visada

            //if (progress < 0)
            //{
            //    meterCount.text = "Heaven";
            //}
            //else if (progress > 1)
            //{
            //    meterCount.text = "The end";
            //}
            //else
            //{
            //    meterCount.text = Mathf.Floor(progress * 1000).ToString() + "m";
            //}

        }
    }
}
