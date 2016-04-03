using UnityEngine;
using System.Collections;

public class EnemyMovePath : MonoBehaviour
{

    //[SerializeField]
    public string pathName = "-";
    public float pathTime = 5.0f;

    // Use this for initialization
    void Start()
    {

        if (pathName == "-") {
            // no path

            // tmp
            iTween.MoveBy(gameObject, iTween.Hash("x", 2, "easeType", "easeInOutExpo", "loopType", "pingPong", "delay", .1));
        } else if (pathName == "down") {
            //this.transform.position += Vector3.down;
        } else {
            // follow path
            iTween.MoveTo(gameObject, iTween.Hash("path", iTweenPath.GetPath(pathName), "time", pathTime));
        }
        
    }
	
}
