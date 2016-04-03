using UnityEngine;
using System.Collections.Generic;

public class BulletTarget : MonoBehaviour
{
    public int Health = 1;

    float delta_color = 0.1f;

    SpriteRenderer sprite;
    //Color original_color;

    //AudioSource audio;
    //public string audio_string;
    public GameObject ago;

    void Start()
    {
        sprite = GetComponentInChildren<SpriteRenderer>();
        //original_color = sprite.color;

        //if(audio_string != "")
        //{
        //    audio = Resources.Load("8BitSounds/" + audio_string) as AudioSource;
        //}
        
        
    }


    void Update()
    {
        if (1.0f > sprite.color.g) {
            var color = sprite.color;
            color.g = color.g + delta_color;
            color.b = color.b + delta_color;
            sprite.color = color;
        }
        
        if (transform.position.y < -10.0f) {
            Destroy(this);
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        var color = sprite.color;
        color.g = 0.0f;
        color.b = 0.0f;
        sprite.color = color;


        Health -= 1;
        if (Health <= 0) {

            if (ago != null) {
                Instantiate(ago, transform.position, Quaternion.identity);
            }
            //if(audio)
            //{
            //    audio.Play();
            //}
            
            
            Destroy(this.gameObject);
        }
    }
}
