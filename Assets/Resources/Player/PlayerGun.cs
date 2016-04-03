using UnityEngine;
using System.Collections.Generic;

public class PlayerGun : MonoBehaviour
{
    const float MaxFiringVectorMag = 4;

    public float BulletSpeed = 0.1f;
    public int BulletPeriod = 1;
    public float SpreadPhase = 0.618f;
    public Vector2 StartFiringVector = new Vector2(0, MaxFiringVectorMag);
    public KeyCode AimKey = KeyCode.Z;
    public KeyCode FireKey = KeyCode.X;

    public Bullet prefab;
    public Color color;
    public Transform BulletOrigin;

    float since_last_shot = 0.0f;
    float last_angle = 0.0f;
    Vector3 last_position;
    float bullet_spread;
    Vector2 firing_vector;

    PlayerTarget playertarget;

    void Start()
    {
        last_position = transform.position;
        ResetFiringVector();
        UpdateFiringVector();
    }

    public void ResetFiringVector()
    {
        firing_vector = StartFiringVector;
    }

    void UpdateFiringVector()
    {
        float mag = firing_vector.magnitude;
        if (mag > MaxFiringVectorMag) {
            firing_vector = firing_vector / mag * MaxFiringVectorMag;
        }

        bullet_spread = 45f / Mathf.Pow(firing_vector.sqrMagnitude, 0.75f);
        
        float r = Mathf.Atan2(firing_vector.y, firing_vector.x) * Mathf.Rad2Deg - 90;
        transform.rotation = Quaternion.Euler(new Vector3(0, 0, r));
    }

    void FixedUpdate()
    {
        if (Input.GetKey(AimKey)) {
            Vector3 dp = transform.position - last_position;
            firing_vector.x -= dp.x;
            firing_vector.y -= dp.y;
            UpdateFiringVector();
        }

        last_position = transform.position;

        if (!playertarget) {
            playertarget = GetComponent<PlayerTarget>();
            return;
        }

        if (playertarget.Despawned) {
            since_last_shot += BulletPeriod;
            return;
        }

        if (Input.GetKey(FireKey)) {
            Fire();
        }
    }

    void Fire()
    {
        since_last_shot += 1;
        if (since_last_shot >= BulletPeriod) {
            Vector2 vel = new Vector2(0, BulletSpeed);

            last_angle = (last_angle + bullet_spread * SpreadPhase) % bullet_spread;
            float angle = last_angle - bullet_spread * 0.5f;

            float r = (this.transform.rotation.eulerAngles.z + angle) * Mathf.Deg2Rad;
            Vector2 tmp = vel;
            vel.x = Mathf.Cos(r) * tmp.x - Mathf.Sin(r) * tmp.y;
            vel.y = Mathf.Sin(r) * tmp.x + Mathf.Cos(r) * tmp.y;

            BulletArena.Spawn(prefab, BulletOrigin.position, vel, false);
            since_last_shot = 0;
        }
    }
}