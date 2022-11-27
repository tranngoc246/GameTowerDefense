using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyGate : AutoLoadComponent
{
    [Header("EnemyGate")]
    [SerializeField] protected Collider gateCollider;
    [SerializeField] protected int gateId = 0;
    [SerializeField] protected GameObject nextGate;
    [SerializeField] protected GameObject spawnPos;

    protected override void LoadComponents()
    {
        base.LoadComponents();
        this.LoadCollider();
        this.LoadGateId();
    }

    private void OnTriggerEnter(Collider other)
    {
        this.MoveEnemy(other.gameObject);
    }

    protected virtual void LoadCollider()
    {
        if (this.gateCollider) return;
        this.gateCollider = GetComponent<Collider>();
        this.gateCollider.isTrigger = true;
        Debug.Log(transform.name + ": LoadCollider");
    }

    protected virtual void LoadGateId()
    {
        if (this.gateId > 0) return;

        string name = gameObject.name;
        this.gateId = int.Parse(name.Replace("EnemyGate", ""));

        int nextId = this.gateId + 1;

        string gateName = "EnemyGate" + nextId;
        this.nextGate = GameObject.Find(gateName);

        string posName = "SpawnPos" + nextId;
        this.spawnPos = GameObject.Find(posName);

        Debug.Log(transform.name + ": LoadGateId");
    }

    protected virtual void MoveEnemy(GameObject enemy)
    {
        if (enemy.layer != MyLayerManager.Ins.layerEnemy) return;

        enemy.transform.position = this.spawnPos.transform.position;
        EnemyCtrl enemyCtrl = enemy.GetComponent<EnemyCtrl>();

        if (this.nextGate) enemyCtrl.enemyMovement.SetTarget(this.nextGate.transform);
    }
}
