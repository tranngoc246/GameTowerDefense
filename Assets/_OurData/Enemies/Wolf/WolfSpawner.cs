using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WolfSpawner : Spawner
{
    public static WolfSpawner Ins;

    [Header("Wolf")]
    [SerializeField] protected float height = 0.5f;

    private void Awake()
    {
        if (WolfSpawner.Ins != null) Destroy(gameObject);
        WolfSpawner.Ins = this;
    }

    private void Reset()
    {
        this.enemyName = "Wolf";
        this.spawnLimit = 2;
    }

    protected override Vector3 SpawnPos()
    {
        float posX = Random.Range(-7.0f, 7.0f);
        return new Vector3(posX, this.height, 0);
    }

    public virtual void WolfDead()
    {
        
    }
}
