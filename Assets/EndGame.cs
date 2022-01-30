using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class EndGame : MonoBehaviour
{

    public GameObject _player;
    public GameObject hitGroudEffect;
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            _player.SetActive(false);
            Instantiate(hitGroudEffect, transform.position, Quaternion.identity);
        }
    }
}
