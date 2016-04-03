using UnityEngine;
using System.Collections.Generic;

public class BulletTarget : MonoBehaviour
{
    void OnTriggerEnter2D(Collider2D other)
    {
        Destroy(this.gameObject);
    }
}
