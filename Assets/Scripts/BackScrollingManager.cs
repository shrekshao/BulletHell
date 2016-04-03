using UnityEngine;
using System.Collections;

public class BackScrollingManager : MonoBehaviour
{

    [SerializeField]
    GameObject[] scrollingWalls;

    public float scrollingSpeed;
    public float scrollingTopY;

    void Update()
    {
        ScrollWalls();
    }

    void ScrollWalls()
    {
        for (int i = 0; i < 2; i++) {
            GameObject wall = scrollingWalls [i];
            wall.transform.position += Vector3.up * scrollingSpeed;
        }

        for (int i = 0; i < 2; i++) {
            GameObject wall = scrollingWalls [i];
            if (Mathf.Abs(wall.transform.position.y) > scrollingTopY) {
                Vector3 p = wall.transform.position;
                p.y = (scrollingWalls [1 - i].transform.position.y + scrollingWalls [1 - i].GetComponent<Renderer>().bounds.size.y);
                wall.transform.position = p;
            }
        }
    }
}
