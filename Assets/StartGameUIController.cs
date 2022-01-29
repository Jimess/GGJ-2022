using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartGameUIController : MonoBehaviour
{
    public void PlayButtonPressed() {
        MenuManager.Instance.StartGame();
    }
}
