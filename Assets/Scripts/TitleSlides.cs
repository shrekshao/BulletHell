using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class TitleSlides : MonoBehaviour
{
    public Sprite[] Slides;
    public string NextScene;

    SpriteRenderer renderer;
    int idx = 0;

    void Start()
    {
        renderer = GetComponent<SpriteRenderer>();
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
            renderer.sprite = Slides [idx];
        }
    }

    void LoadSlide()
    {
        if (idx >= Slides.Length) {
            SceneManager.LoadScene(NextScene);
        } else {
            renderer.sprite = Slides [idx];
        }
    }
}
