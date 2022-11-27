using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageSender : AutoLoadComponent
{
    [Header("DamageSender")]
    [SerializeField] protected int damage = 1;

    private void OnCollisionEnter(Collision col)
    {
        this.SendDamage(col);
    }

    private void OnTriggerEnter(Collider other)
    {
        this.SendDamage(other.gameObject);
    }

    protected virtual void SendDamage(Collision col)
    {
        DamageReceiver damageReceiver = col.gameObject.GetComponent<DamageReceiver>();
        if (damageReceiver == null) return;
        damageReceiver.Receive(this.damage);
    }

    protected virtual void SendDamage(GameObject colliderObj)
    {
        DamageReceiver damageReceiver = colliderObj.GetComponent<DamageReceiver>();
        if (damageReceiver == null) return;
        damageReceiver.Receive(this.damage, this);
    }

    public virtual void SetDamage(int damage)
    {
        this.damage = damage;
    }
}
