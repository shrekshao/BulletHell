using UnityEngine;
using System.Collections.Generic;

public class Bullet : MonoBehaviour
{
    public Vector2 velocity;
    public BulletArena arena;
    public bool hostile;

    void Start()
    {
    }

    void Update()
    {
        this.transform.position += new Vector3(velocity.x, velocity.y, 0);
        if (!this.arena.bounds.Contains(this.arena.transform.InverseTransformPoint(this.transform.position))) {
            Destroy(this.gameObject);
        }
    }
}
