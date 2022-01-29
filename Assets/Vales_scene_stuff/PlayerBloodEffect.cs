using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBloodEffect : MonoBehaviour
{
    public GameObject bloodEffect;
    // Start is called before the first frame update
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (!collision.gameObject.CompareTag("Player") && !collision.gameObject.CompareTag("Game_bounds"))
        {
            Instantiate(bloodEffect, transform.position, Quaternion.identity);
        }
    }
}
