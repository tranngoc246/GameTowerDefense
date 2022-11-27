using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StatueDamageReceiver : DamageReceiver
{
    [SerializeField] protected StatueCtrl statueCtrl;

    protected override void LoadComponents()
    {
        base.LoadComponents();
        this.LoadStatueCtrl();
    }

    protected virtual void LoadStatueCtrl()
    {
        if (this.statueCtrl) return;
        this.statueCtrl = GetComponent<StatueCtrl>();
        //Debug.Log(transform.name + ": LoadStatueCtrl");
    }

    protected override void ResetValue()
    {
        base.ResetValue();
        this.HP = 2;
        this.MaxHP = 2;
    }

    public override void Receive(int damage, DamageSender sender)
    {
        int senderLayer = sender.gameObject.layer;
        if (senderLayer != MyLayerManager.Ins.layerEnemy) return;
        this.Receive(damage);
    }

    protected override void Despawn()
    {
        this.statueCtrl.statue.gameObject.SetActive(false);
        this.statueCtrl.gravestone.gameObject.SetActive(true);
        gameObject.layer = MyLayerManager.Ins.layerStatueBroken;
    }

    public override bool Heal()
    {
        bool result = base.Heal();
        if (!result) return false;

        this.statueCtrl.statue.gameObject.SetActive(true);
        this.statueCtrl.gravestone.gameObject.SetActive(false);
        gameObject.layer = MyLayerManager.Ins.layerStatue;

        return true;
    }
}
