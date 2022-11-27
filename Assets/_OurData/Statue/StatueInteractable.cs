using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StatueInteractable : PlayerInteractable
{
    [Header("Statue")]
    public StatueCtrl statueCtrl;

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

    public override void Interact()
    {
        base.Interact();
        if (this.statueCtrl.damageReceiver.IsHPFull()) this.statueCtrl.statueLevel.LevelUp(1);
        else this.statueCtrl.damageReceiver.Heal();
    }
}
