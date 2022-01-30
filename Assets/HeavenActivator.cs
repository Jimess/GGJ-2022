using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeavenActivator : MonoBehaviour
{
    private bool isHeaven = false;

    private void OnTriggerExit2D(Collider2D collision) {
        if (collision.CompareTag("Player") && !isHeaven) {
            isHeaven = true;
            LevelManager.Instance.GameOver();
        }
    }
}
