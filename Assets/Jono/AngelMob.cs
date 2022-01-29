using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AngelMob : MonoBehaviour, IMob
{
    // Start is called before the first frame update
    public float speedAdjustment = 1000f;

    public void OnCollide()
    {
        print("Angel");
    }

    public float GetSpeedAdjustment()
    {
        return speedAdjustment;
    }
}
