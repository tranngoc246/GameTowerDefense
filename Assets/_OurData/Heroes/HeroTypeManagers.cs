using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeroTypeManagers : MonoBehaviour
{
    public static HeroTypeManagers Ins;
    public HeroesManager[] heroesManagers;

    private void Awake()
    {
        if (HeroTypeManagers.Ins != null) Destroy(gameObject);
        else HeroTypeManagers.Ins = this;
    }

    private void Reset()
    {
        this.LoadComponent();
    }

    protected virtual void LoadComponent()
    {
        if (heroesManagers.Length > 0) return;

        GameObject obj = GameObject.Find("HeroTypeManagers");
        heroesManagers = obj.GetComponentsInChildren<HeroesManager>();
    }
}
