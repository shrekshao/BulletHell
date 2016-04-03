using UnityEngine;
using System.Collections;

public class PlayerTarget : MonoBehaviour
{
    const float RespawnTime = 2.0f;
    const float FlickerTime = 0.15f;

    public Transform SpawnPoint;

    public bool Despawned { get; private set; }

    float despawntime = 0;
    SpriteRenderer sprite;

    void Start()
    {
        sprite = GetComponentInChildren<SpriteRenderer>();
        Despawned = false;
    }

    void Update()
    {
        if (Despawned) {
            var color = sprite.color;
            color.a = ((int)((Time.time - despawntime) / FlickerTime)) % 2 == 0 ? 0.5f : 1f;
            sprite.color = color;
        }

        if (Despawned && Time.time > despawntime + RespawnTime) {
            Despawned = false;
            var color = sprite.color;
            color.b = 1;
            color.g = 1;
            color.a = 1;
            sprite.color = color;
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (!Despawned) {
            this.transform.position = SpawnPoint.transform.position;
            this.transform.rotation = SpawnPoint.transform.rotation;
            var color = sprite.color;
            color.b = 0;
            color.g = 0;
            color.a = 1;
            sprite.color = color;
            despawntime = Time.time;
            Despawned = true;
        }
    }
}
