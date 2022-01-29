using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class WorldSpin : MonoBehaviour
{
    [Header("Main camera")]
    public GameObject _cam;

    public void Spin()
    {
        SpinCamera();
        ChangeGravity();
    }

    private void SpinCamera()
    {
        float camRotation = _cam.transform.rotation.z > 0 ? -180f : 180f;
        _cam.transform.DOBlendableRotateBy(new Vector3(transform.rotation.x, transform.rotation.y, transform.rotation.z + camRotation), 2f).SetEase(Ease.InOutQuint);
    }

    private void ChangeGravity()
    {
        DOTween.To(() => Physics2D.gravity.y, newGravity => Physics2D.gravity = new Vector2(0, newGravity), -Physics2D.gravity.y, 2f).SetEase(Ease.InOutQuint);
    }

}
