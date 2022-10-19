using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : Spawner
{
    [Header("Enemy")]
    [SerializeField] protected List<Transform> spawnPos;
    [SerializeField] protected List<string> nameEnemies;
    [SerializeField] protected Transform target;

    protected override void ResetValue()
    {
        base.ResetValue();
        this.nameEnemies.Add("Turtle");
    }

    protected override void LoadComponents()
    {
        base.LoadComponents();
        this.LoadSpawnPos();
    }

    protected virtual void LoadSpawnPos()
    {
        if (this.spawnPos.Count > 0) return;
        foreach(Transform child in transform)
        {
            this.spawnPos.Add(child);
            child.gameObject.SetActive(false);
        }

        Debug.Log(transform.name + ": LoadSpawnPos");
    }

    protected override Vector3 SpawnPos()
    {
        int rand = Random.Range(0, this.spawnPos.Count);
        Transform pos = this.spawnPos[rand];

        return pos.position;
    }

    protected override void BeforeSpawn()
    {
        base.BeforeSpawn();
        this.enemyName = this.GetEnemyName();
    }

    protected override void AfterSpawn(Transform obj)
    {
        base.AfterSpawn(obj);
        EnemyCtrl enemyCtrl = obj.GetComponent<EnemyCtrl>();
        enemyCtrl.enemyMoverment.SetTarget(this.target);
    }

    protected virtual string GetEnemyName()
    {
        return this.nameEnemies[0];
    }

    protected override float SpawnDelay()
    {
        int level = GameLevelManager.Ins.GetLevel();
        this.finalSpawnDelay = this.spawnDelay - (level * 0.1f);
        if (this.finalSpawnDelay <= 0) return 0;
        return this.finalSpawnDelay;
    }

    protected override int SpawnLimit()
    {
        int level = GameLevelManager.Ins.GetLevel();
        this.finalSpawnLimit = this.spawnLimit * level;
        return this.finalSpawnLimit;
    }
}
