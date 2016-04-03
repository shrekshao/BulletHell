using UnityEngine;

public class GlobalState : MonoBehaviour
{
    float scalelevel = 0.0f;

    void OnGUI()
    {
        var guistyle = new GUIStyle();
        guistyle.normal.textColor = Color.black;
        GUI.Label(new Rect(10, 10, 150, 100), Mathf.Pow(1.05f, scalelevel).ToString("f2"), guistyle);
    }

    void Update()
    {
        scalelevel += Input.mouseScrollDelta.y;
        scalelevel = Mathf.Clamp(scalelevel, -30f, 15f);
        Time.timeScale = Mathf.Pow(1.05f, scalelevel);
    }
}
