using UnityEngine;
using System.Collections.Generic;

public class PlayerGun : MonoBehaviour
{
    const float MaxFiringVectorMag = 4;

    public float BulletSpeed = 0.1f;
    public int BulletPeriod = 1;
    public float SpreadPhase = 0.618f;
    public Vector2 StartFiringVector = new Vector2(0, MaxFiringVectorMag);
    public float FiringVectorMoveSpeed = 0.1f;
    public KeyCode AimKey = KeyCode.Z;
    public KeyCode FireKey = KeyCode.X;
    public KeyCode BulletSwapKey = KeyCode.C;
    public float BulletSwapCost = 500;

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

        bullet_spread = 45f / Mathf.Pow(firing_vector.sqrMagnitude, 0.25f);
        
        float r = Mathf.Atan2(firing_vector.y, firing_vector.x) * Mathf.Rad2Deg - 90;
        transform.rotation = Quaternion.Euler(new Vector3(0, 0, r));
    }

    void FixedUpdate()
    {
        if (Input.GetKey(AimKey)) {
            float move_x = 0, move_y = 0;
            move_x -= Input.GetKey(KeyCode.LeftArrow) ? 1 : 0;
            move_x += Input.GetKey(KeyCode.RightArrow) ? 1 : 0;
            move_y -= Input.GetKey(KeyCode.DownArrow) ? 1 : 0;
            move_y += Input.GetKey(KeyCode.UpArrow) ? 1 : 0;
            move_x *= FiringVectorMoveSpeed;
            move_y *= FiringVectorMoveSpeed;

            firing_vector.x -= move_x;
            firing_vector.y -= move_y;
            UpdateFiringVector();
        }

        last_position = transform.position;

        if (!playertarget) {
            playertarget = GetComponent<PlayerTarget>();
            return;
        }

        if (GlobalState.PlayerDead) {
            since_last_shot += BulletPeriod;
            return;
        }

        if (Input.GetKeyDown(BulletSwapKey)) {
            if (GlobalState.Instance.UsePower(BulletSwapCost)) {
                BulletArena.Reverse();
            }
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