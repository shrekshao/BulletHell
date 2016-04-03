using UnityEngine;
using System.Collections;

public class EnemyBehaviorBase : MonoBehaviour {


    //[SerializeField] Sprite bullet;

    Transform fireTransform;

    //temp
    Transform playerTransform;

	// Use this for initialization
	void Start () {
        fireTransform = transform.Find("fire_position");
        playerTransform = GameObject.Find("Player").transform;
	}
	
	// Update is called once per frame
	void Update () {
        Aim(playerTransform);

        if(Random.value < 0.05)
        {
            Fire();
        }
	}



    const float BulletSpeed = 0.2f;
    const int BulletPeriod = 1;
    const float BulletSpread = 0.15f;

    public Bullet prefab;

    float since_last_shot = 0.0f;
    float last_angle = 0.0f;

    void Fire()
    {
        //Instantiate(bullet, fireTransform.position, this.transform.rotation);

        Vector2 vel = new Vector2(last_angle - BulletSpread * 0.5f, 1.0f);
        last_angle = (last_angle + BulletSpread * 0.618f) % BulletSpread;
        vel.Normalize();
        vel *= BulletSpeed;

        float r = (this.transform.rotation.eulerAngles.z - 90.0f) * Mathf.Deg2Rad;
        Vector2 tmp = vel;
        vel.x = Mathf.Cos(r) * tmp.x - Mathf.Sin(r) * tmp.y;
        vel.y = Mathf.Sin(r) * tmp.x + Mathf.Cos(r) * tmp.y;


        BulletArena.Spawn(prefab, fireTransform.position, vel, true);
        since_last_shot = 0;
    }


    void Aim(Transform target)
    {
        if(target)
        {
            float r = Mathf.Atan2(target.position.y - transform.position.y, target.position.x - transform.position.x) * Mathf.Rad2Deg;
            transform.rotation = Quaternion.Euler(new Vector3(0, 0, r));
        }
        
    }

    void Deconstruct()
    {
        EnemyMovePath emp = GetComponent<EnemyMovePath>();

        if(emp)
        {
            Destroy(GameObject.Find(emp.pathName));
        }

        Destroy(gameObject);
    }
}
