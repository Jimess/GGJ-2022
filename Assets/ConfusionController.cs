using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class ConfusionController : MonoBehaviour
{
    [SerializeField] private RectTransform confusionImage;
    [SerializeField] private CanvasGroup fadableCG;

    private Tween confusionSpinTween;

    private void Awake() {
        CollisionManager.onCollision += StartConfusion;
    }

    private void OnDestroy() {
        CollisionManager.onCollision -= StartConfusion;
    }

    public void StartConfusion() {

        if (confusionSpinTween != null && confusionSpinTween.IsPlaying()) {
            confusionSpinTween.Kill(false);
        }

        Sequence seq = DOTween.Sequence();
        seq.Append(fadableCG.DOFade(1f, 0.25f).SetEase(Ease.OutBounce));
        seq.Append(confusionImage.DOBlendableRotateBy(new Vector3(0, 0, 960), 1.5f, RotateMode.LocalAxisAdd));
        seq.Append(fadableCG.DOFade(0f, 0.25f).SetEase(Ease.OutBounce));

        confusionSpinTween = seq;
    }

    public void EndConfusion() {
        //confusionSpinTween.Kill();
    }
}
