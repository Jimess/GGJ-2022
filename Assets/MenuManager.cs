using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuManager : Singleton<MenuManager>
{
    [SerializeField] private Animator startGamePlayerAnim;

    public void StartGame() {
        startGamePlayerAnim.SetTrigger("Jump");
        Invoke("LoadGame", 2f);
    }

    public void LoadGame() {
        Initiate.Fade("Game", Color.black, 2f);
    }
}
