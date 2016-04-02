using UnityEngine;
using System.Collections.Generic;

public class BulletArena : MonoBehaviour
{
    List<Bullet> bullets;
    public Bounds bounds;

    static int layer_playerbullet = LayerMask.NameToLayer("PlayerBullet");
    static int layer_enemybullet = LayerMask.NameToLayer("EnemyBullet");

    void Start()
    {
    }

    void Update()
    {
    }

    public void Spawn(Bullet prefab, Color color, Vector2 pos, Vector2 vel, bool hostile)
    {
        var bullet = Instantiate(prefab);
        bullet.gameObject.layer = hostile ? layer_enemybullet : layer_playerbullet;
        bullet.GetComponent<SpriteRenderer>().color = color;
        bullet.transform.position = pos;
        bullet.velocity = vel;
        bullet.arena = this;
    }
}
