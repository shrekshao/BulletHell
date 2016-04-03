using UnityEngine;
using System.Collections.Generic;

public class BulletTarget : MonoBehaviour
{
    public int Health = 1;

    void OnTriggerEnter2D(Collider2D other)
    {
        Health -= 1;
        if (Health <= 0) {
            Destroy(this.gameObject);
        }
    }
}
