using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyCtrl : AutoLoadComponent
{
    [Header("Enemy")]
    public Rigidbody enemyRgbody;
    public Transform enemy;
    public EnemyMoverment enemyMovement;
    public DamageReceiver damageReceiver;
    public EnemyLevel enemyLevel;

    protected override void LoadComponents()
    {
        base.LoadComponents();
        this.LoadRigidbody();
        this.LoadEnemy();
        this.LoadDamageReceiver();
    }

    protected virtual void LoadDamageReceiver()
    {
        if (this.damageReceiver) return;
        this.damageReceiver = GetComponent<DamageReceiver>();
        this.enemyLevel = GetComponent<EnemyLevel>();
    }

    protected virtual void LoadRigidbody()
    {
        if (this.enemyRgbody) return;
        this.enemyRgbody = GetComponent<Rigidbody>();
        this.enemyRgbody.constraints = RigidbodyConstraints.FreezePositionZ | RigidbodyConstraints.FreezeRotationX | RigidbodyConstraints.FreezeRotationY | RigidbodyConstraints.FreezeRotationZ;

        Debug.Log(transform.name + ": LoadRigidbody");
    }

    protected virtual void LoadEnemy()
    {
        if (this.enemy) return;
        this.enemy = transform.Find("Enemy");
        this.enemyMovement = transform.Find("EnemyMovement").GetComponent<EnemyMoverment>();

        Debug.Log(transform.name + ": LoadEnemy");
    }
}
