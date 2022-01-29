using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;
using DG.Tweening;

public class LookaheadController : MonoBehaviour
{
    public float orthoChangeModifier;
    public float maxOrthoSize;
    public float orthoChangeIntensity;

    float orthoSize;
    float currentOrthoSize;
    CinemachineVirtualCamera vcam;

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
        float newOrthoSize = orthoSize + Mathf.Abs(velocityChange.y) * orthoChangeModifier;
        if (newOrthoSize >= maxOrthoSize)
        {
            newOrthoSize = maxOrthoSize;
        }

        DOTween.To(() => currentOrthoSize, x => currentOrthoSize = x, newOrthoSize, orthoChangeIntensity).OnUpdate(SetOrthoSize);
    }

    private void SetOrthoSize()
    {
        vcam.m_Lens.OrthographicSize = currentOrthoSize;
    }

    // Start is called before the first frame update
    void Start()
    {
        vcam = GetComponent<CinemachineVirtualCamera>();
        orthoSize = vcam.m_Lens.OrthographicSize;
        currentOrthoSize = orthoSize;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
