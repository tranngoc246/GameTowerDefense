using System.Collections;
using System.Collections.Generic;
using UnityEngine;

class DespawnByDistance : AutoLoadComponent
{
    [SerializeField] protected Transform target;
    [SerializeField] protected float distance = 0;
    [SerializeField] protected float distanceLimit = 50;

    private void Update()
    {
        this.Checking();
    }

    protected virtual void Checking()
    {
        this.LoadTarget();
        this.distance = Vector3.Distance(transform.position, this.target.position);
        if (this.distance < this.distanceLimit) return;
        this.Despawn();
    }

    protected virtual void LoadTarget()
    {
        this.target = PlayerManager.Ins.currentHero.transform;
    }

    protected virtual void Despawn()
    {
        ObjPoolManager.Ins.Despawm(transform);
    }
}
