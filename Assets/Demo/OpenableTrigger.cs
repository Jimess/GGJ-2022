using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OpenableTrigger : MonoBehaviour
{

    // collideris informuoja manageri -> menedzeris daro kitus dalykus
    // collideris kviecia UI controlleri -> UI controlleris parodo kazkoki interface mygtukui paspaust -> paspaudus mygtuka informuojam menedzeri su paspaustu mgytuku ir atidarom
    // collideris tiesiog savo tevui sako viskas ok

    private void OnTriggerEnter2D(Collider2D collision) {
        if (collision.CompareTag("Player")) {
            print("working");
            GetComponentInParent<IOpenable>().Open();
            gameObject.SetActive(false);
        }
        
    }
}
