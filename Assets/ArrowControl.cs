using UnityEngine;
using System.Collections;

public class ArrowControl : MonoBehaviour
{
    public float MoveSpeed = 0.1f;

    // Use this for initialization
    void Start()
    {
	
    }
	
    // Update is called once per frame
    void Update()
    {
        float move_x = 0, move_y = 0;
        move_x -= Input.GetKey(KeyCode.LeftArrow) ? 1 : 0;
        move_x += Input.GetKey(KeyCode.RightArrow) ? 1 : 0;
        move_y -= Input.GetKey(KeyCode.DownArrow) ? 1 : 0;
        move_y += Input.GetKey(KeyCode.UpArrow) ? 1 : 0;
        if (move_x != 0 || move_y != 0) {
            var mv = new Vector3(move_x, move_y, 0);
            mv = mv.normalized * MoveSpeed;
            this.transform.localPosition += mv;
        }
    }
}
