using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MobOnCollisionTrigger : MonoBehaviour
{
    public delegate void OnCollision(IMob collisionMob);
    public static OnCollision onCollision;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Mob"))
        {
            collision.GetComponentInParent<IMob>().OnCollide();
            onCollision(collision.GetComponentInParent<IMob>());
        }
    }
}
