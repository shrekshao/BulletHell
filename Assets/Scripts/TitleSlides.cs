using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class TitleSlides : MonoBehaviour
{
    public Sprite[] Slides;
    public string NextScene;

    SpriteRenderer slide;
    int idx = 0;

    void Start()
    {
        slide = GetComponent<SpriteRenderer>();
        LoadSlide();

        Cursor.visible = true;
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape)) {
            // Forward
            idx += 1;
            LoadSlide();
        }
        if (Input.GetMouseButtonDown(1) || Input.GetKeyDown(KeyCode.Z) || Input.GetKeyDown(KeyCode.LeftArrow)) {
            // Backward
            idx = Mathf.Max(idx - 1, 0);
            LoadSlide();
        }
    }

    void LoadSlide()
    {
        if (idx >= Slides.Length) {
            SceneManager.LoadScene(NextScene);
        } else {
            slide.sprite = Slides [idx];
        }
    }
}
