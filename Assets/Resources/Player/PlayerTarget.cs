﻿using UnityEngine;
using System.Collections;

public class PlayerTarget : MonoBehaviour
{
    public float RespawnTime = 2.0f;
    public float FlickerTime = 0.15f;

    public Transform SpawnPoint;

    float despawntime = 0;
    SpriteRenderer sprite;

    void Start()
    {
        sprite = GetComponentInChildren<SpriteRenderer>();
        GlobalState.PlayerDead = false;
    }

    void Update()
    {
        if (GlobalState.PlayerDead) {
            var color = sprite.color;
            color.a = ((int)((Time.time - despawntime) / FlickerTime)) % 2 == 0 ? 0.5f : 1f;
            sprite.color = color;
        }

        if (GlobalState.PlayerDead && Time.time > despawntime + RespawnTime) {
            GlobalState.PlayerDead = false;
            var color = sprite.color;
            color.b = 1;
            color.g = 1;
            color.a = 1;
            sprite.color = color;
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (GlobalState.PlayerDead) {
            return;
        }
        this.transform.position = SpawnPoint.transform.position;
        this.transform.rotation = SpawnPoint.transform.rotation;
        this.GetComponent<PlayerGun>().ResetFiringVector();
        var color = sprite.color;
        color.b = 0;
        color.g = 0;
        color.a = 1;
        sprite.color = color;
        despawntime = Time.time;
        GlobalState.PlayerDead = true;
    }
}
