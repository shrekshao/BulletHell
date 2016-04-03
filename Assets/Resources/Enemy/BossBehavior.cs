using UnityEngine;
using System.Collections;

public class BossBehavior : MonoBehaviour
{

    //// Use this for initialization
    //void Start () {

    //}
	
    // Update is called once per frame
    public float battle_line = 2.0f;
    public float down_speed = 0.01f;

    public Transform[] BulletOrigin;

    //temp
    PlayerTarget playerTarget;

    public float BulletSpeed = 0.1f;
    public int BulletPeriod = 1;
    public float BulletSpread = 90;
    public float RechargeTime = 5;
    public float FiringTime = 5;
    public float SpreadFactor = 0.618f;
    public float BulletPhaseOffset = 0;

    public Bullet prefab;

    float time_modeswitch = 0.0f;
    int since_last_shot = 0;
    float last_angle = 0.0f;
    bool firing = false;

    void Start()
    {
        GameObject p = GameObject.Find("Player");
        if (p) {
            playerTarget = p.GetComponent<PlayerTarget>();
        }

        time_modeswitch = BulletPhaseOffset;
    }

    void Update()
    {
        if (this.transform.position.y > battle_line) {
            this.transform.position += Vector3.down * down_speed;
        }
    }

    void FixedUpdate()
    {
        if (playerTarget != null && !GlobalState.PlayerDead) {
            TryFire();
        }
    }

    void TryFire()
    {
        if (firing) {
            if (Time.time - time_modeswitch > FiringTime) {
                firing = false;
                time_modeswitch = Time.time;
            }
        } else {
            if (Time.time - time_modeswitch > RechargeTime) {
                firing = true;
                time_modeswitch = Time.time;
            }
        }

        if (firing) {
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
            vel.y = -vel.y;

            //BulletArena.Spawn(prefab, BulletOrigin.position, vel, true);
            //BulletArena.Spawn(prefab, BulletOrigin[Random.Range(0,BulletOrigin.Length)].position, vel, true);

            foreach (Transform b in BulletOrigin) {
                BulletArena.Spawn(prefab, b.position, vel, true);
            }

            since_last_shot = 0;
        }
    }
}
