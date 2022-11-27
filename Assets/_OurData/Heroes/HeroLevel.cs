using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeroLevel : Level
{
    [Header("Hero")]
    [SerializeField] protected HeroCtrl heroCtrl;

    protected override void LoadComponents()
    {
        base.LoadComponents();
        this.LoadHeroCtrl();
        this.LoadLevel();
    }

    protected virtual void LoadHeroCtrl()
    {
        if (this.heroCtrl) return;
        this.heroCtrl = GetComponent<HeroCtrl>();
        Debug.Log(transform.name + ": LoadHeroCtrl");
    }

    protected virtual void LoadLevel()
    {
        if (this.level > 0) return;
        string name = gameObject.name;
        string className = this.heroCtrl.heroProfile.HeroClass();

        name = name.Replace(className, "");
        int level = int.Parse(name);
        this.SetLevel(level);

        Debug.Log(transform.name + ": LoadLevel");
    }
}