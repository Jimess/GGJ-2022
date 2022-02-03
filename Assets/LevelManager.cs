using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class LevelManager : Singleton<LevelManager>
{
    [SerializeField] private GameObject playerPrefab;
    [SerializeField] private CinemachineVirtualCamera playerVCam;
    [SerializeField] private GameObject spawnLocation;

    [SerializeField] private GatesOfHeaven gatesOfHeavenScript;

    private void Start() {
        playerPrefab.transform.position = spawnLocation.transform.position;
        Application.targetFrameRate = 60;

        //ir paleisti kitus menedzerius jei reikia (PVZ.: progressManager)
        // ProgressManager.Instance.StartCalculatingDistance()
    }

    public void OpenGatesOfHeaven() {
        gatesOfHeavenScript.OpenGate();
    }

    //public void GetAllMobs(out List<GameObject> angels, out List<GameObject> devils) {

    //}

    public void GameOver() {
        print("gameOver son, heave is da sheet");
        Destroy(playerPrefab.GetComponentInChildren<FreeFallController>());
        GameObject.FindObjectOfType<FreeFallController>().DisableCharacter();
        Initiate.Fade("Heaven", Color.white, 2f);
        //playerPrefab.GetComponentInChildren<FreeFallController>()?.DisableCharacter();
    }

    public void loadSceneAfterDeath()
    {
        Initiate.Fade("Menu", Color.black, 2f);
    }
}
