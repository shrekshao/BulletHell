using UnityEngine;
using System.Collections;

public class EnemyMovePath : MonoBehaviour {

    [SerializeField]
    string pathName = "New Path 1";

	// Use this for initialization
	void Start () {
        iTween.MoveTo(gameObject, iTween.Hash("path", iTweenPath.GetPath(pathName), "time", 5));
    }
	
}
