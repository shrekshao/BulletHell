using UnityEngine;
using System.Collections;

public class DestroyAfterAudio : MonoBehaviour {

	// Use this for initialization
	void Start () {
        var audio = GetComponent<AudioSource>();
        audio.Play();
        Destroy(this.gameObject, audio.clip.length);
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
