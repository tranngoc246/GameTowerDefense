using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Level : AutoLoadComponent
{
    [Header("Level")]
    [SerializeField] protected bool canLevelUp = false;
    [SerializeField] protected int level;

    public virtual int GetLevel()
    {
        return this.level;
    }

    public virtual int LevelUp(int up)
    {
        if (!this.canLevelUp) return this.level;
        this.level += up;
        return this.level;
    }

    public virtual int SetLevel(int newLevel)
    {
        this.level = newLevel;
        return this.level;
    }
}

