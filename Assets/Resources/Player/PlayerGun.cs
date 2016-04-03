using UnityEngine;
using System.Collections.Generic;

public class PlayerGun : MonoBehaviour
{
    public float BulletSpeed = 0.1f;
    public int BulletPeriod = 1;
    public float BulletSpread = 90;
    public float SpreadFactor = 0.618f;

    public Bullet prefab;
    public Color color;
    public Transform BulletOrigin;

    float since_last_shot = 0.0f;
    float last_angle = 0.0f;

    PlayerTarget playertarget;

    void FixedUpdate()
    {
        if (!playertarget) {
            playertarget = GetComponent<PlayerTarget>();
            return;
        }

        if (playertarget.Despawned) {
            since_last_shot += BulletPeriod;
            return;
        }

        if (Input.GetKey(KeyCode.Space)) {
            Fire();
        }
    }

    void Fire()
    {
        since_last_shot += 1;
        if (since_last_shot >= BulletPeriod) {
            Vector2 vel = new Vector2(0, BulletSpeed);

            last_angle = (last_angle + BulletSpread * SpreadFactor) % BulletSpread;
            float angle = last_angle - BulletSpread * 0.5f;

            float r = (this.transform.rotation.eulerAngles.z + angle) * Mathf.Deg2Rad;
            Vector2 tmp = vel;
            vel.x = Mathf.Cos(r) * tmp.x - Mathf.Sin(r) * tmp.y;
            vel.y = Mathf.Sin(r) * tmp.x + Mathf.Cos(r) * tmp.y;

            BulletArena.Spawn(prefab, BulletOrigin.position, vel, false);
            since_last_shot = 0;
        }
    }
}