using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyDamageReceiver : DamageReceiver
{
    [Header("Enemy")]
    [SerializeField] protected EnemyCtrl enemyCtrl;

    protected override void LoadComponents()
    {
        base.LoadComponents();
        this.LoadEnemyCtrl();
    }
    protected override void ResetValue()
    {
        base.ResetValue();
        this.MaxHP = 2;
    }

    protected virtual void LoadEnemyCtrl()
    {
        if (enemyCtrl != null) return;
        this.enemyCtrl = GetComponent<EnemyCtrl>();
        Debug.Log(transform.name + ": LoadEnemyCtrl");
    }

    protected override void Despawn()
    {
        base.Despawn();
        this.enemyCtrl.enemyMovement.ChangeAnimation("Idle");
    }
}
