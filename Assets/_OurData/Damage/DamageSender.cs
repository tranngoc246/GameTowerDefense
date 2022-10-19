using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageSender : MonoBehaviour
{
    [Header("DamageSender")]
    [SerializeField] protected int damage = 1;

    private void OnCollisionEnter(Collision col)
    {
        this.SendDamage(col);
    }

    protected virtual void SendDamage(Collision col)
    {
        DamageReceiver damageReceiver = col.gameObject.GetComponent<DamageReceiver>();
        if (damageReceiver == null) return;
        damageReceiver.Receive(this.damage);
    }
}
