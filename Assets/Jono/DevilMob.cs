using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DevilMob : MonoBehaviour, IMob
{
    public float speedAdjustment = -1000f;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnCollide()
    {
        print("Devil");
    }

    public float GetSpeedAdjustment()
    {
        return speedAdjustment;
    }
}
