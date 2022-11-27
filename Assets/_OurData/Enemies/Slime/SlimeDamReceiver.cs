using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlimeDamReceiver : DamageReceiver
{
    protected override void ResetValue()
    {
        base.ResetValue();
        this.HP = 2;
        this.MaxHP = 2;
    }
    protected override void Despawn()
    {
        base.Despawn();
        SlimeSpawner.Ins.SlimeDead();
    }
}
