using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerScriptVALE : MonoBehaviour
{

    public GameObject bloodEffect;

    public GameObject GroundEffect;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            WorldSpinManager.Instance.Spin();
            Instantiate(bloodEffect, transform.position, Quaternion.identity);
        }
    }
}
