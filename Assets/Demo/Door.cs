using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class Door : MonoBehaviour, IOpenable {
    public void Open() {
        transform.DOScaleY(0f, 1f);
        //transform.doMove
    }
}
