using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class WorldSpinManager : Singleton<WorldSpinManager>
{
    //[Header("Main camera")]
    //public GameObject _cam;

    [Header("Rotate the WorldUp ovveride for cinemachine brain")]
    public Transform worldUp;
    private bool _isGoingToEnd = true;

    public delegate void CameraRotated();
    public static CameraRotated OnCameraRotation;

    public void Spin()
    {
        SpinCamera();
        ChangeGravity();
        restartCollisionCount();
    }

    private void SpinCamera()
    {
        //float camRotation = _cam.transform.rotation.z > 0 ? -180f : 180f;
        //_cam.transform.DOBlendableRotateBy(new Vector3(transform.rotation.x, transform.rotation.y, transform.rotation.z + camRotation), 2f).SetEase(Ease.InOutQuint);
        _isGoingToEnd = !_isGoingToEnd;
        worldUp.DOLocalRotate(new Vector3(0, 0, 180f), 2f, RotateMode.LocalAxisAdd).SetEase(Ease.InOutQuint);
        OnCameraRotation?.Invoke();
    }

    private void ChangeGravity()
    {
        DOTween.To(() => Physics2D.gravity.y, newGravity => Physics2D.gravity = new Vector2(0, newGravity), -Physics2D.gravity.y, 2f).SetEase(Ease.InOutQuint);
    }

    private void restartCollisionCount()
    {
        CollisionManager.Instance.restartCollisionCount();
    }

    public bool isGoingToEnd() {
        return _isGoingToEnd;
    }

}
