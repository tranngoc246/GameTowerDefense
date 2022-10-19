using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WolfDamReceiver : DamageReceiver
{
    private void Reset()
    {
        this.HP = 3;
        this.MaxHP = 3;
    }
    protected override void Despawn()
    {
        base.Despawn();
        WolfSpawner.Ins.WolfDead();
    }
}
