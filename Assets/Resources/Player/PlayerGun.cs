using UnityEngine;
using System.Collections.Generic;

public class PlayerGun : MonoBehaviour
{
    const float BulletSpeed = 0.2f;
    const int BulletPeriod = 1;
    const float BulletSpread = 0.15f;

    public BulletArena arena;
    public Bullet prefab;
    public Color color;
    public bool hostile;

    float since_last_shot = 0.0f;
    float last_angle = 0.0f;

    void Start()
    {
    }

    void FixedUpdate()
    {
        since_last_shot += 1;

        if (Input.GetKey(KeyCode.Space)) {
            if (since_last_shot >= BulletPeriod) {
                Vector2 vel = new Vector2(last_angle - BulletSpread * 0.5f, 1.0f);
                last_angle = (last_angle + BulletSpread * 0.618f) % BulletSpread;
                vel.Normalize();
                vel *= BulletSpeed;
                arena.Spawn(prefab, this.transform.position, vel, hostile);
                since_last_shot = 0;
            }
        }
    }
}