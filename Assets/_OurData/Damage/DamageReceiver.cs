using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageReceiver : MonoBehaviour
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

    public virtual void Receive(int damage)
    {
        this.HP -= damage;
        this.Dying();
    }

    protected virtual void Dying()
    {
        if (!IsDead()) return;

        this.HP = this.MaxHP;
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
        ObjPoolManager.Ins.Despawm(transform);
        ScoreManager.Ins.Kill();
        ScoreManager.Ins.GoldAdd(1);
    }
}
