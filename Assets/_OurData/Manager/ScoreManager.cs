using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class ScoreManager : MonoBehaviour
{
    public static ScoreManager Ins;

    [Header("Score")]
    [SerializeField] protected int gold;
    [SerializeField] protected int kill;

    private void Awake()
    {
        if (ScoreManager.Ins != null) Destroy(gameObject);
        ScoreManager.Ins = this;
    }

    public virtual void Kill()
    {
        this.kill++;
    }

    public virtual int GetKill()
    {
        return this.kill;
    }

    public virtual void GoldAdd(int count)
    {
        this.gold += count;
    }

    public virtual bool GoldDeduct(int count)
    {
        if (this.gold < count) return false;
        this.gold -= count;
        return true;
    }

    public virtual int GetGold()
    {
        return this.gold;
    }

    public virtual void FromJson(string jsonString)
    {
        ScoreData obj = JsonUtility.FromJson<ScoreData>(jsonString);
        if (obj == null) return;
        this.gold = obj.gold;
        this.kill = obj.kill;
    }
}
