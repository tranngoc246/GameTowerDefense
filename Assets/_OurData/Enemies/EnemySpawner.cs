using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : Spawner
{
    [Header("Enemy")]
    [SerializeField] protected List<string> nameEnemies;
    [SerializeField] protected Transform target;

    protected override void LoadComponents()
    {
        base.LoadComponents();
        this.LoadTarget();
    }
    protected override void ResetValue()
    {
        base.ResetValue();
        this.nameEnemies.Add("Turtle");
    }

    protected virtual void LoadTarget()
    {
        if (this.target) return;

        this.target = GameObject.Find("EnemyGate1").transform;
    }

    protected override Vector3 SpawnPos()
    {
        return SpawnPosManager.Ins.GetPos(0).position;
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
        enemyCtrl.enemyMovement.SetTarget(this.target);

        int gameLevel = GameLevelManager.Ins.GetLevel();
        enemyCtrl.enemyLevel.SetLevel(gameLevel);
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
