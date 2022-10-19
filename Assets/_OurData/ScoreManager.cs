using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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

    public virtual void GoldAdd(int count)
    {
        this.gold += count;
    }

    public virtual void GoldDeduct(int count)
    {
        this.gold -= count;
    }
}
