using UnityEngine;
using System.Collections;

public class EnemyBehaviorStupid : EnemyBehaviorBase {

    public float flyingSpeed = 0.1f;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
        this.transform.position += Vector3.down * flyingSpeed;
	}
}
