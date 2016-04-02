using UnityEngine;
using System.Collections;

public class EnemyBehaviorBase : MonoBehaviour {


    [SerializeField] Sprite bullet;

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
	}

    void Fire()
    {
        Instantiate(bullet, fireTransform.position, this.transform.rotation);
    }

    void Aim(Transform target)
    {
        float r = Mathf.Atan2(target.position.y - transform.position.y, target.position.x - transform.position.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(new Vector3(0, 0, r));
    }
}
