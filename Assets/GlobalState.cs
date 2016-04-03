using UnityEngine;
using UnityEngine.SceneManagement;

public class GlobalState : MonoBehaviour
{
    public static GlobalState Instance;

    public KeyCode SpeedFast = KeyCode.LeftControl;
    public float SpeedFastCost = -2.3f;
    public KeyCode SpeedSlow = KeyCode.LeftShift;
    public float SpeedSlowCost = 1.2f;
    public float PowerRechargeRate = 0.2f;
    public float MaxPower = 1000;

    public KeyCode WinKey = KeyCode.Escape;
    public string NextScene = "";

    public static bool PlayerDead { get; set; }

    GUIStyle guistyle = new GUIStyle();
    public static float power;

    void Start()
    {
        Instance = this;
        power = MaxPower;
        guistyle.normal.textColor = Color.black;
        guistyle.fontSize = 20;

        Cursor.visible = false;
    }

    void OnGUI()
    {
        GUI.Label(new Rect(10, 10, 150, 100), string.Format("TIME: x{0:f2}", Time.timeScale), guistyle);
        GUI.Label(new Rect(10, 90, 150, 100), string.Format("POWER: {0:f0}/{1:f0}", power, MaxPower), guistyle);
    }

    void Update()
    {
        if (Input.GetKey(WinKey)) {
            SceneManager.LoadScene(NextScene);
        }

        UsePower(-PowerRechargeRate);

        if (Input.GetKey(SpeedFast) && !PlayerDead && UsePower(SpeedFastCost)) {
            Time.timeScale = 1.5f;
        } else if (Input.GetKey(SpeedSlow) && !PlayerDead && UsePower(SpeedSlowCost)) {
            Time.timeScale = 0.25f;
        } else {
            Time.timeScale = 1.0f;
        }
    }

    public bool UsePower(float p)
    {
        if (0 > power - p || power - p > MaxPower) {
            return false;
        }
        power -= p;
        return true;
    }
}
