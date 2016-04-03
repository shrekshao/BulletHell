using UnityEngine;
using System.Collections.Generic;

public class BulletArena : MonoBehaviour
{
    public static BulletArena Instance { get; private set; }

    HashSet<Bullet> bullets = new HashSet<Bullet>();
    public Bounds bounds;

    void Start()
    {
        Instance = this;
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.A)) {
            Reverse();
        }
    }

    public static void Spawn(Bullet prefab, Vector2 pos, Vector2 vel, bool hostile)
    {
        var bullet = Instantiate(prefab);
        bullet.transform.position = pos;
        bullet.velocity = vel;
        bullet.Hostile = hostile;
        Instance.bullets.Add(bullet);
    }

    public static void Despawn(Bullet bullet)
    {
        Instance.bullets.Remove(bullet);
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
