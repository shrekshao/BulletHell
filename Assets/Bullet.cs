using UnityEngine;
using System.Collections.Generic;

public class Bullet : MonoBehaviour
{
    public Vector2 velocity;
    public BulletArena arena;
    bool hostile;

    public bool Hostile {
        get { return hostile; }
        set {
            hostile = value;
            this.gameObject.layer = hostile ? layer_enemybullet : layer_playerbullet;
            this.GetComponent<SpriteRenderer>().color = hostile ? Color.red : Color.blue;
        }
    }

    static int layer_playerbullet = LayerMask.NameToLayer("PlayerBullet");
    static int layer_enemybullet = LayerMask.NameToLayer("EnemyBullet");

    void Start()
    {
    }


    void Update()
    {
        this.transform.position += new Vector3(velocity.x, velocity.y, 0);
        if (!this.arena.bounds.Contains(this.arena.transform.InverseTransformPoint(this.transform.position))) {
            this.arena.Despawn(this);
        }
    }
}
