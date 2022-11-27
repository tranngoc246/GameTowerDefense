using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyDamageSender : DamageSender
{
    [Header("Enemy")]
    [SerializeField] protected EnemyCtrl enemyCtrl;

    protected override void LoadComponents()
    {
        base.LoadComponents();
        this.LoadEnemyCtrl();
    }

    protected virtual void LoadEnemyCtrl()
    {
        if (this.enemyCtrl) return;
        this.enemyCtrl = GetComponent<EnemyCtrl>();
        Debug.Log(transform.name + ": LoadEnemyCtrl");
    }

    protected override void SendDamage(GameObject colliderObj)
    {
        DamageReceiver damageReceiver = colliderObj.GetComponent<DamageReceiver>();
        if (damageReceiver == null) return;

        int currentHP = this.enemyCtrl.damageReceiver.GetHP();
        damageReceiver.Receive(currentHP, this);
        this.enemyCtrl.damageReceiver.Receive(currentHP);
    }
}
