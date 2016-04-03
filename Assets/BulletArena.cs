using UnityEngine;
using System.Collections.Generic;

public class BulletArena : MonoBehaviour
{
    HashSet<Bullet> bullets = new HashSet<Bullet>();
    public Bounds bounds;

    void Start()
    {
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.A)) {
            Reverse();
        }
    }

    public void Spawn(Bullet prefab, Vector2 pos, Vector2 vel, bool hostile)
    {
        var bullet = Instantiate(prefab);
        bullet.transform.position = pos;
        bullet.velocity = vel;
        bullet.arena = this;
        bullet.Hostile = hostile;
        bullets.Add(bullet);
    }

    public void Despawn(Bullet bullet)
    {
        bullets.Remove(bullet);
        Destroy(bullet.gameObject);
    }

    void Reverse()
    {
        foreach (var bullet in bullets) {
            bullet.velocity = -bullet.velocity;
            bullet.Hostile = !bullet.Hostile;
        }
    }
}
