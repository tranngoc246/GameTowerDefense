using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlimeSpawner : Spawner
{
    public static SlimeSpawner Ins;

    [Header("Slime")]
    [SerializeField] protected float height = 0.45f;

    private void Awake()
    {
        if (SlimeSpawner.Ins != null) Destroy(gameObject);
        SlimeSpawner.Ins = this;
    }

    private void Reset()
    {
        this.enemyName = "Slime";
        this.spawnLimit = 1;
    }

    protected override Vector3 SpawnPos()
    {
        float posX = Random.Range(-7.0f, 7.0f);
        return new Vector3(posX, this.height, 0);
    }

    public virtual void SlimeDead()
    {
        this.spawnDelay += 2;
    }
}
