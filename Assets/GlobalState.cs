using UnityEngine;

public class GlobalState : MonoBehaviour
{
    float scalelevel = 0.0f;
    GUIStyle guistyle = new GUIStyle();

    void Start()
    {
        guistyle.normal.textColor = Color.black;
        guistyle.fontSize = 20;
    }

    void OnGUI()
    {
        GUI.Label(new Rect(10, 10, 150, 100), string.Format("TIME x{0:f2}", Time.timeScale), guistyle);
    }

    void Update()
    {
        scalelevel += Input.mouseScrollDelta.y * 2;
        scalelevel = Mathf.Clamp(scalelevel, -30f, 15f);
        Time.timeScale = Mathf.Clamp(Mathf.Pow(1.1f, scalelevel), 0.25f, 2f);
    }
}
