using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyLevel : MyLevel
{
    [Header("EnemyLevel")]
    [SerializeField] protected EnemyCtrl enemyCtrl;

    protected override void LoadComponents()
    {
        base.LoadComponents();
        this.LoadEnemyCtrl();
    }

    protected virtual void LoadEnemyCtrl()
    {
        if (enemyCtrl != null) return;
        this.enemyCtrl = GetComponent<EnemyCtrl>();
        Debug.Log(transform.name + ": LoadEnemyCtrl");
    }

    public override int SetLevel(int newLevel)
    {
        base.SetLevel(newLevel);

        this.level = newLevel;
        int newHP = newLevel + 2;

        this.enemyCtrl.damageReceiver.SetHP(newHP);
        this.enemyCtrl.damageReceiver.SetHPMax(newHP);

        return this.level;
    }
}