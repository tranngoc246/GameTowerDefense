using Assets.HeroEditor.Common.CharacterScripts;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeroEditorFix:AutoLoadComponent
{
    public static HeroEditorFix Ins;

    private void Awake()
    {
        if (HeroEditorFix.Ins) Destroy(gameObject);
        HeroEditorFix.Ins = this;
    }

    public virtual Bullet CreateBullets(FirearmFire firearmFire, Firearm firearm)
    {
        Transform tBullet = ObjPoolManager.Ins.Spawn("Bullet", firearm.FireTransform);
        Bullet bullet = tBullet.GetComponent<Bullet>();

        HeroCtrl heroCtrl = this.GetHeroCtrl(firearmFire);
        int level = heroCtrl.heroLevel.GetLevel();

        DamageSender damageSender = tBullet.GetComponent<DamageSender>();
        damageSender.SetDamage(level);

        tBullet.gameObject.SetActive(true);

        return bullet;
    }

    protected virtual HeroCtrl GetHeroCtrl(FirearmFire firearmFire)
    {
        Transform hero = firearmFire.transform.parent.parent.parent.parent.parent.parent.parent.parent;
        return hero.GetComponent<HeroCtrl>();
    }
}
