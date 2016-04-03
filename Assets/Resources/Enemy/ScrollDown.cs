using UnityEngine;
using System.Collections;

public class ScrollDown : MonoBehaviour {

    public float scrollDownSpeed = 0.005f;

	// Update is called once per frame
	void Update () {
        this.transform.position += Vector3.down * scrollDownSpeed;

    }
}
