using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MyLayerManager : AutoLoadComponent
{
    public static MyLayerManager Ins;
    public int layerHero;
    public int layerGround;
    public int layerCeiling;
    public int layerBullet;
    public int layerEnemy;

    private void Awake()
    {
        if (MyLayerManager.Ins != null) Destroy(gameObject);
        MyLayerManager.Ins = this;

        this.LoadComponents();

        Physics.IgnoreLayerCollision(this.layerHero, this.layerHero, true);
        Physics.IgnoreLayerCollision(this.layerHero, this.layerEnemy, true);
        Physics.IgnoreLayerCollision(this.layerHero, this.layerBullet, true);
        Physics.IgnoreLayerCollision(this.layerEnemy, this.layerEnemy, true);
    }

    protected override void LoadComponents()
    {
        base.LoadComponents();
        this.GetLayers();
    }

    protected virtual void GetLayers()
    {
        if (this.layerHero > 0) return;

        this.layerHero = LayerMask.NameToLayer("Hero");
        this.layerGround = LayerMask.NameToLayer("Ground");
        this.layerCeiling = LayerMask.NameToLayer("Ceiling");
        this.layerBullet = LayerMask.NameToLayer("Bullet");
        this.layerEnemy = LayerMask.NameToLayer("Enemy");

        if (this.layerHero < 0) Debug.LogError("Layer Hero is missing");
        if (this.layerGround < 0) Debug.LogError("Layer Ground is missing");
        if (this.layerCeiling < 0) Debug.LogError("Layer Ceiling is missing");
        if (this.layerBullet < 0) Debug.LogError("Layer Bullet is missing");
        if (this.layerEnemy < 0) Debug.LogError("Layer Enemy is missing");
    }
}
