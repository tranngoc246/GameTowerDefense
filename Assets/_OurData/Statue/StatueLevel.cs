using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StatueLevel : MyLevel
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

    private void OnTriggerStay(Collider other)
    {
        this.CheckActor(other.gameObject, true);
    }

    private void OnTriggerExit(Collider other)
    {
        this.CheckActor(other.gameObject, false);
    }

    protected virtual void CheckActor(GameObject gameObject, bool status)
    {
        if (this.canLevelUp == status) return;

        int layer = gameObject.layer;
        if (layer != MyLayerManager.Ins.layerHero) return;

        this.canLevelUp = status;

        PlayerManager playerManager = PlayerManager.Ins;
        if (status) playerManager.playerInput.interactable = this.statueCtrl.statueInteractable;
        else playerManager.playerInput.interactable = null;

        //Debug.Log(transform.name + ": Level up " + status.ToString());
    }

    public override int LevelUp(int up)
    {
        base.LevelUp(up);

        int newHP = this.level * 10;
        this.statueCtrl.damageReceiver.SetHPMax(newHP);
        this.statueCtrl.damageReceiver.SetHP(newHP);

        return this.level;
    }
}

