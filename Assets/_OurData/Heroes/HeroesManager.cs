using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeroesManager : AutoLoadComponent
{
    [Header("Hero")]
    public List<HeroCtrl> heroes = new List<HeroCtrl>();

    protected override void LoadComponents()
    {
        this.LoadHeros();
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

    public GameObject GetHero()
    {
        GameObject hero = this.heroes[0].gameObject;
        GameObject obj = Instantiate(hero,Vector3.zero,transform.rotation);
        obj.SetActive(true);
        return obj;
    }
}
