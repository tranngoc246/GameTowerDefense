using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameLevelManager : MonoBehaviour
{
    public static GameLevelManager Ins;

    [Header("GameLevel")]
    [SerializeField] protected int level = 0;
    [SerializeField] protected int secondPerLevel = 20;
    [SerializeField] protected float timer = 0;

    private void Awake()
    {
        if (GameLevelManager.Ins != null) Destroy(gameObject);
        GameLevelManager.Ins = this;
    }

    private void FixedUpdate()
    {
        this.timer += Time.fixedDeltaTime;
        this.LevelCaculate();
    }

    protected virtual void LevelCaculate()
    {
        this.level = (int)Mathf.FloorToInt(this.timer / this.secondPerLevel);
        this.level++;
    }

    public virtual int GetLevel()
    {
        return this.level;
    }
}
