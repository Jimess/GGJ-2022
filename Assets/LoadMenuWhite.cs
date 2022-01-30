using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoadMenuWhite : MonoBehaviour
{
    private void Start() {
        GetComponent<Animator>().SetTrigger("Heaven");
    }

    public void LoadMenu() {
        Initiate.Fade("Menu", Color.white, 2f);
    }
}
