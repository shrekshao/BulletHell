using UnityEngine;
using System.Collections;

public class EnemyMovePath : MonoBehaviour {

    //[SerializeField]
    public string pathName = "-";

	// Use this for initialization
	void Start () {

        if(pathName == "-")
        {
            // no path

            // tmp
            iTween.MoveBy(gameObject, iTween.Hash("x", 2, "easeType", "easeInOutExpo", "loopType", "pingPong", "delay", .1));
        }
        else
        {
            // follow path
            iTween.MoveTo(gameObject, iTween.Hash("path", iTweenPath.GetPath(pathName), "time", 5));
        }
        
    }
	
}
