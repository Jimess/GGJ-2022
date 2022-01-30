using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundScript : MonoBehaviour
{
    public GameObject groundEffect;
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            Instantiate(groundEffect, collision.contacts[0].point, Quaternion.FromToRotation(Vector3.down, collision.contacts[0].normal));
        }
    }
}
