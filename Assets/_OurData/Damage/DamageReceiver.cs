using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageReceiver : AutoLoadComponent
{
    [Header("DamageReceiver")]
    [SerializeField] protected int HP = 1;
    [SerializeField] protected int MaxHP = 1;
    [SerializeField] protected string deadEffect = "EnemyDeath1";

    private void Start()
    {
        this.HP = this.MaxHP;
    }

    public virtual bool IsDead()
    {
        return this.HP <= 0;
    }

    public virtual int GetHP()
    {
        return this.HP;
    }

    public virtual void SetHP(int hp)
    {
        this.HP = hp;
    }

    public virtual void SetHPMax(int hp)
    {
        this.MaxHP = hp;
    }

    public virtual bool IsHPFull()
    {
        return this.HP == this.MaxHP;
    }

    public virtual void Receive(int damage)
    {
        this.HP -= damage;
        this.Dying();
    }

    public virtual void Receive(int damage, DamageSender sender)
    {
        this.Receive(damage);
    }

    protected virtual void Dying()
    {
        if (!IsDead()) return;

        this.ShowDeadEffect();
        this.Despawn();
    }

    protected virtual void ShowDeadEffect()
    {
        Transform effect = ObjPoolManager.Ins.Spawn(this.deadEffect, transform.position, transform.rotation);
        effect.gameObject.SetActive(true);
    }

    protected virtual void Despawn()
    {
        this.HP = this.MaxHP;
        ObjPoolManager.Ins.Despawm(transform);
        ScoreManager.Ins.Kill();
        ScoreManager.Ins.GoldAdd(1);
    }

    public virtual bool Heal()
    {
        int gold = ScoreManager.Ins.GetGold();
        if (gold <= 0) return false;

        int loseHP = this.MaxHP - this.HP;
        int cost = loseHP;
        if (gold < loseHP) cost = gold;

        this.HP += cost;
        ScoreManager.Ins.GoldDeduct(cost);
        return true;
    }
}
