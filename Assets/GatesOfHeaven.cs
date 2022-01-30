using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class GatesOfHeaven : MonoBehaviour
{

    private float closedY;
    [SerializeField] GameObject heaveStuffToActivate;

    // Start is called before the first frame update
    void Start()
    {
        closedY = transform.localScale.y;
        transform.localScale = new Vector3(transform.localScale.x, 0);
        Invoke("CloseGate", 2f);
    }

    public void OpenGate() {
        transform.DOScaleY(0, 1f);
        heaveStuffToActivate.SetActive(true);
    }

    public void CloseGate() {
        transform.DOScaleY(closedY, 1f);
    }
}
