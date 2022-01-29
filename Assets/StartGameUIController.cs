using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartGameUIController : MonoBehaviour
{
    [SerializeField] private Animator startGamePlayerAnim;

    public void StartGame() {
        LevelManager.Instance.StartGame();
        startGamePlayerAnim.SetTrigger("Jump");
        Invoke("LoadGame", 2f);
    }

    public void LoadGame() {
        Initiate.Fade("Aurio", Color.black, 2f);
    }
}
