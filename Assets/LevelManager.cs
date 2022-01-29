using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class LevelManager : Singleton<LevelManager>
{
    [SerializeField] private GameObject playerPrefab;
    [SerializeField] private CinemachineVirtualCamera playerVCam;

    private void Start() {
        // sukurti player prefab instance jam skirtoje vietoje
        //GameObject player = Instantiate(playerPrefab, )

        // ijungti jeigu reikia jo controlsus

        // sutvarkyti vCam kad sektu zaideja
        //playerVCam.targ

        //ir paleisti kitus menedzerius jei reikia (PVZ.: progressManager)
        // ProgressManager.Instance.StartCalculatingDistance()
    }
}
