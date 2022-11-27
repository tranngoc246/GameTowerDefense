using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnPosManager : AutoLoadComponent
{
    public static SpawnPosManager Ins;

    [Header("SpawnPos")]
    [SerializeField] protected List<Transform> spawnPos;

    private void Awake()
    {
        if (SpawnPosManager.Ins != null) Destroy(gameObject);
        SpawnPosManager.Ins = this;
    }

    protected override void LoadComponents()
    {
        base.LoadComponents();
        this.LoadSpawnPos();
    }

    protected virtual void LoadSpawnPos()
    {
        if (this.spawnPos.Count > 0) return;

        foreach(Transform child in transform)
        {
            this.spawnPos.Add(child);
        }
        Debug.Log(transform.name + ": LoadSpawnPos");
    }

    public virtual Transform GetPos(int index)
    {
        if (this.spawnPos.Count <= 0) return null;
        return this.spawnPos[index];
    }
}