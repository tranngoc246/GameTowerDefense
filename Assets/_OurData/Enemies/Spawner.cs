using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : AutoLoadComponent
{
    [Header("Spawner")]
    [SerializeField] protected string enemyName = "Cube";
    [SerializeField] protected int spawnLimit = 2;
    [SerializeField] protected int finalSpawnLimit = 2;
    [SerializeField] protected float spawnTimer = 0;
    [SerializeField] protected float spawnDelay = 2f;
    [SerializeField] protected float finalSpawnDelay = 2f;
/*
    void Start()
    {
        StartCoroutine(WaitForSeconds(1.0f));
    }

    IEnumerator WaitForSeconds(float seconds)
    {
        yield return new WaitForSeconds(seconds);
        Spawning();
    }*/

    private void FixedUpdate()
    {
        this.Spawning();
    }

    protected virtual void Spawning()
    {
        if (!this.CanSpawn()) return;

        this.spawnTimer += Time.fixedDeltaTime;
        if (this.spawnTimer < this.SpawnDelay()) return;
        this.spawnTimer = 0;

        this.BeforeSpawn();
        Transform obj = ObjPoolManager.Ins.Spawn(this.EnemyName(), this.SpawnPos(), transform.rotation, transform);
        obj.gameObject.SetActive(true);
        this.AfterSpawn(obj);
    }

    protected virtual string EnemyName()
    {
        return this.enemyName;
    }

    protected virtual void BeforeSpawn() { }
    protected virtual void AfterSpawn(Transform obj) { }

    protected virtual Vector3 SpawnPos()
    {
        float posX = Random.Range(-7.0f, 7.0f);
        float posY = Random.Range(0f, 5.0f);
        return new Vector3(posX, posY, 0);
    }

    protected virtual bool CanSpawn()
    {
        int childCount = this.GetChildCountActive();
        if (childCount >= this.SpawnLimit()) return false;
        return true;
    }

    protected virtual int GetChildCountActive()
    {
        int count = 0;
        for(int i = 0; i < transform.childCount; i++)
        {
            if (transform.GetChild(i).gameObject.activeSelf) count++;
        }
        return count;
    }

    protected virtual int SpawnLimit()
    {
        this.finalSpawnLimit = this.spawnLimit;
        return this.finalSpawnLimit;
    }

    protected virtual float SpawnDelay()
    {
        this.finalSpawnDelay = this.spawnDelay;
        return this.finalSpawnDelay;
    }
}
