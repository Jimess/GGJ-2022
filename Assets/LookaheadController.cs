using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class LookaheadController : MonoBehaviour
{
    private void Awake()
    {
        FreeFallController.onVelocityChange += CameraMove;
    }

    private void OnDestroy()
    {
        FreeFallController.onVelocityChange -= CameraMove;
    }

    public void CameraMove(Vector2 velocityChange)
    {
        // GetComponent<CinemachineVirtualCamera>();
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
