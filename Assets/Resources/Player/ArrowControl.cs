using UnityEngine;

public class ArrowControl : MonoBehaviour
{
    public float MoveSpeed = 0.1f;

    PlayerTarget playertarget;

    void Update()
    {
        if (!playertarget) {
            playertarget = GetComponent<PlayerTarget>();
            return;
        }

        if (playertarget.Despawned) {
            return;
        }

        float move_x = 0, move_y = 0;
        move_x -= Input.GetKey(KeyCode.LeftArrow) ? 1 : 0;
        move_x += Input.GetKey(KeyCode.RightArrow) ? 1 : 0;
        move_y -= Input.GetKey(KeyCode.DownArrow) ? 1 : 0;
        move_y += Input.GetKey(KeyCode.UpArrow) ? 1 : 0;
        if (move_x != 0 || move_y != 0) {
            var mv = new Vector3(move_x, move_y, 0);
            mv = mv.normalized * MoveSpeed * Time.timeScale;
            this.transform.localPosition += mv;
        }
    }
}
