using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;


public class WorldSpinUIController : MonoBehaviour
{

    [SerializeField] CanvasGroup cg;
    [SerializeField] private RectTransform spinTF;

    private void Awake() {
        WorldSpinManager.OnCameraRotation += SpinWorldUI;
    }

    private void OnDestroy() {
        WorldSpinManager.OnCameraRotation -= SpinWorldUI;
    }

    public void SpinWorldUI() {
        Sequence seq = DOTween.Sequence();

        seq.Append(spinTF.DOScale(Vector3.one, 0.25f).From(Vector3.zero).SetEase(Ease.InOutQuint));
        seq.Join(cg.DOFade(1f, 0.25f).SetEase(Ease.InOutQuint));
        seq.Append(spinTF.DOBlendableLocalRotateBy(new Vector3(0, 0, 180), 1.5f).SetEase(Ease.InOutQuint));
        seq.Append(spinTF.DOScale(Vector3.zero, 0.25f).SetEase(Ease.InOutQuint));
        seq.Join(cg.DOFade(0f, 0.25f).SetEase(Ease.InOutQuint));
    }
}
