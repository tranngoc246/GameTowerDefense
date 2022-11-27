using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StatueCtrl : AutoLoadComponent
{
    [Header("StatusCtrl")]
    public StatueLevel statueLevel;
    public StatueDamageReceiver damageReceiver;
    public StatueInteractable statueInteractable;
    public Transform statue;
    public Transform gravestone;
    public Collider statueCollider;

    protected override void LoadComponents()
    {
        base.LoadComponents();
        this.LoadStatueInteractable();
        this.LoadStatueLevel();
        this.LoadStatueDamageReceiver();
        this.LoadStatue();
    }

    protected virtual void LoadStatue()
    {
        if (this.statue != null) return;

        this.statue = transform.Find("Statue");
        this.gravestone = transform.Find("Gravestone");
        this.statueCollider = GetComponent<Collider>();
        this.gravestone.gameObject.SetActive(false);
    }

    protected virtual void LoadStatueInteractable()
    {
        if (this.statueInteractable) return;
        this.statueInteractable = GetComponent<StatueInteractable>();
        Debug.Log(transform.name + ": LoadStatueInteractable");
    }

    protected virtual void LoadStatueDamageReceiver()
    {
        if (this.damageReceiver) return;
        this.damageReceiver = GetComponent<StatueDamageReceiver>();
        Debug.Log(transform.name + ": LoadStatueDamageReceiver");
    }

    protected virtual void LoadStatueLevel()
    {
        if (this.statueLevel) return;
        this.statueLevel = GetComponent<StatueLevel>();
        Debug.Log(transform.name + ": LoadStatueLevel");
    }
}
