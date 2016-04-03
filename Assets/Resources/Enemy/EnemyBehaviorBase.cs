using UnityEngine;
using System.Collections;

public class EnemyBehaviorBase : MonoBehaviour
{


    //[SerializeField] Sprite bullet;

    public Transform BulletOrigin;

    //temp
    PlayerTarget playerTarget;

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
        if (playerTarget.Despawned) {
            return;
        }

        Aim(playerTarget);
    }

    void FixedUpdate()
    {
        TryFire();
    }

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

    void TryFire()
    {
        if (firing) {
            if (Time.time - time_modeswitch > FiringTime) {
                firing = false;
                Debug.Log(firing);
                time_modeswitch = Time.time;
            }
        } else {
            if (Time.time - time_modeswitch > RechargeTime) {
                firing = true;
                Debug.Log(firing);
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

            BulletArena.Spawn(prefab, BulletOrigin.position, vel, true);
            since_last_shot = 0;
        }
    }


    void Aim(PlayerTarget target)
    {
        float r = Mathf.Atan2(target.transform.position.y - transform.position.y,
                      target.transform.position.x - transform.position.x) * Mathf.Rad2Deg - 90;
        transform.rotation = Quaternion.Euler(new Vector3(0, 0, r));
    }

    void Deconstruct()
    {
        EnemyMovePath emp = GetComponent<EnemyMovePath>();

        if (emp) {
            Destroy(GameObject.Find(emp.pathName));
        }

        Destroy(gameObject);
    }
}
