using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeroesManager : AutoLoadComponent
{
    [Header("Hero")]
    public List<HeroCtrl> heroes = new List<HeroCtrl>();
    public HeroProfile heroProfile;

    protected override void LoadComponents()
    {
        this.LoadHeros();
        this.LoadHeroProfile();
    }

    protected void LoadHeroProfile()
    {
        if (this.heroProfile) return;

        this.heroProfile = GetComponent<HeroProfile>();
        Debug.Log(transform.name + ": LoadHeroProfile");
    }

    protected virtual void LoadHeros()
    {
        if (this.heroes.Count > 0) return;
        foreach (HeroCtrl heroCtrl in transform.GetComponentsInChildren<HeroCtrl>())
        {
            this.heroes.Add(heroCtrl);
            heroCtrl.gameObject.SetActive(false);
        }
    }

    public HeroCtrl GetHero()
    {
        return this.GetHero(0);
    }

    public virtual HeroCtrl GetHero(int index)
    {
        if (index >= this.heroes.Count) return null;

        GameObject heroObj = this.heroes[index].gameObject;
        GameObject hero = Instantiate(heroObj);
        hero.gameObject.SetActive(true);
        HeroCtrl heroCtrl = hero.GetComponent<HeroCtrl>();
        heroCtrl.heroesManager = this;
        return heroCtrl;
    }

    public virtual HeroCtrl GetNextHero(int currentLevel)
    {
        return this.GetHero(currentLevel);
    }

    public virtual bool TryGetNextHero(int index)
    {
        if (index >= this.heroes.Count) return false;
        return true;
    }
}
