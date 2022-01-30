using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using TMPro;

public class DialogUIController : MonoBehaviour
{
    [SerializeField] private CanvasGroup cg;
    [SerializeField] private RectTransform tf;
    [SerializeField] private TextMeshProUGUI textMesh;

    public void ShowDialog( float duration, string text) {
        Sequence seq = DOTween.Sequence();

        seq.AppendCallback(() => {
            textMesh.text = text;
        });
        seq.Append(cg.DOFade(1f, 0.25f).SetEase(Ease.InOutQuint));
        seq.Join(tf.DOScale(Vector3.one, 0.25f).From(Vector3.zero).SetEase(Ease.InOutQuint));
        seq.AppendInterval(duration);
        seq.Append(cg.DOFade(0f, 0.25f).SetEase(Ease.InOutQuint));
        seq.Join(tf.DOScale(Vector3.one, 0.25f).SetEase(Ease.InOutQuint));
    }

    //private void Update() {
    //    if (Input.GetKeyDown(KeyCode.H)) {
    //        ShowDialog(3f, "OPA");
    //    }
    //}
}
