using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogManager : Singleton<DialogManager>
{
    [SerializeField] private DialogUIController dialogController;

    public void ShowDialog(float duration, string text) {
        dialogController.ShowDialog(duration, text);
    }
}
