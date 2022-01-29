using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IMob
{
    public void OnCollide();
    public float GetSpeedAdjustment();
}
