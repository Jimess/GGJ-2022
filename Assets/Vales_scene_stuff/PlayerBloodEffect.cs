using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBloodEffect : MonoBehaviour
{
    public GameObject bloodEffect;
    // Start is called before the first frame update
    private void Start()
    {
        CollisionManager.onCollision += bloodSplatter;
    }

    private void bloodSplatter()
    {
        Instantiate(bloodEffect, transform.position, Quaternion.identity);
    }

}
