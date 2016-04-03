using UnityEngine;
using System.Collections;

public class BackScrollingManager : MonoBehaviour {

    [SerializeField]
    GameObject[] scrollingWalls;

    public float scrollingSpeed;
    public float scrollingTopY;

    // Use this for initialization
    void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
        ScrollWalls();

    }

    void ScrollWalls()
    {

        for (int i = 0; i < 2; i++)
        {
            GameObject wall = scrollingWalls[i];
            wall.transform.position += Vector3.up * scrollingSpeed;
        }

        for (int i = 0; i < 2; i++)
        {
            GameObject wall = scrollingWalls[i];
            if (wall.transform.position.y > scrollingTopY)
            //if(!wall.GetComponent<Renderer>().isVisible)
            {
                Vector3 p = wall.transform.position;
                p.y = (scrollingWalls[1 - i].transform.position.y
                    - scrollingWalls[1 - i].GetComponent<Renderer>().bounds.size.y / 2.0f);
                wall.transform.position = p;
            }
        }
    }
}
