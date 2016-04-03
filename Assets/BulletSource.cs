using UnityEngine;
using System.Collections.Generic;

public class BulletSource : MonoBehaviour
{
    public BulletArena arena;
    public Bullet prefab;
    public Color color;
    public bool hostile;

    void Start()
    {
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space)) {
            arena.Spawn(prefab, this.transform.position, new Vector2(0, 0.1f), hostile);
        }
    }
}
