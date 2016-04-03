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
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0)) {
            // Forward
            idx += 1;
            LoadSlide();
        }
        if (Input.GetMouseButtonDown(1)) {
            // Backward
            idx = Mathf.Max(idx - 1, 0);
            slide.sprite = Slides [idx];
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
