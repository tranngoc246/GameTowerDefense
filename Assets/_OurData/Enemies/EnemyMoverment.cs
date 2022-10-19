using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMoverment : AutoLoadComponent
{
    [Header("Enemy")]
    [SerializeField] protected EnemyCtrl enemyCtrl;
    [SerializeField] protected Transform target;
    [SerializeField] protected float speed = 2f;
    [SerializeField] protected Vector3 direction = Vector3.zero;

    private void FixedUpdate()
    {
        this.Turning();
    }

    private void Update()
    {
        this.Moving();
    }

    private void OnEnable()
    {
        this.OnRenew();
    }

    protected virtual void OnRenew()
    {
        this.enemyCtrl.enemyRgbody.velocity = Vector3.zero;
        this.enemyCtrl.enemyRgbody.angularVelocity = Vector3.zero;
    }

    protected override void LoadComponents()
    {
        base.LoadComponents();
        this.LoadEnemyCtrl();
    }

    protected virtual void LoadEnemyCtrl()
    {
        if (this.enemyCtrl) return;
        this.enemyCtrl = transform.parent.GetComponent<EnemyCtrl>();
        Debug.Log(transform.name + ": LoadEnemyCtrl");
    }

    protected virtual void Moving()
    {
        if (this.IsTargetActive()) return;
        Vector3 vec = Time.deltaTime * this.speed * this.GetDirection();
        this.enemyCtrl.enemyRgbody.MovePosition(transform.position + vec);
    }

    protected virtual Vector3 GetDirection()
    {
        this.direction.x = 0;
        if (this.target.position.x > transform.position.x) this.direction.x = 1;
        if (this.target.position.x < transform.position.x) this.direction.x = -1;
        return this.direction;
    }

    protected virtual bool IsTargetActive()
    {
        return this.target == null;
    }

    protected virtual void Turning()
    {
        if (this.direction.x == 0) return;
        Vector3 scale = this.enemyCtrl.enemy.transform.localScale;
        scale.x = Mathf.Abs(scale.x);
        scale.x *= this.direction.x;

        this.enemyCtrl.enemy.transform.localScale = scale;
    }

    public virtual void SetTarget(Transform target)
    {
        this.target = target;
    }
}
