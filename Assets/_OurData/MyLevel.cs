using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MyLevel: Level
{
    [Header("MyLevel")]
    [SerializeField] protected int levelCost = 10;

    public override int LevelUp(int up)
    {
        base.LevelUp(up);

        this.levelCost *= up;
        int gold = ScoreManager.Ins.GetGold();
        if (gold < levelCost) return this.level;
        ScoreManager.Ins.GoldDeduct(levelCost);
        this.level += up;
        this.levelCost = this.level * 10;
        //Debug.Log(transform.name + ": Up " + this.level);
        return this.level;
    }
}
