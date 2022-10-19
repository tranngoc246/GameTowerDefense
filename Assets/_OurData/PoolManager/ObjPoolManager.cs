using PathologicalGames;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjPoolManager : AutoLoadComponent
{
    public static ObjPoolManager Ins;
    [SerializeField] protected string poolName = "ObjPool";
    [SerializeField] protected List<Transform> objs = new();
    [SerializeField] protected SpawnPool pool;

    private void Awake()
    {
        if (Ins != null) Destroy(gameObject);
        ObjPoolManager.Ins = this;
    }

    private void Start()
    {
        this.AddObjToPool();
    }

    protected override void LoadComponents()
    {
        this.LoadPool();
        this.LoadObjs();
    }

    protected virtual void LoadPool()
    {
        if (pool != null) return;

        GameObject obj = GameObject.Find(this.poolName);
        this.pool = obj.GetComponent<SpawnPool>();
        this.pool.poolName = this.poolName;

    }

    protected virtual void LoadObjs()
    {
        if (objs.Count > 0) return;

        foreach(Transform child in transform)
        {
            this.objs.Add(child);
            child.gameObject.SetActive(false);
        }
    }

    protected virtual void AddObjToPool()
    {
        foreach (Transform obj in this.objs)
        {
            PrefabPool prefabPool = new PrefabPool(obj)
            {
                preloadAmount = 1
            };
            bool isAlreadyPool = this.pool.GetPrefabPool(prefabPool.prefab) == null;
            if (isAlreadyPool) this.pool.CreatePrefabPool(prefabPool);
        }
    }

    public virtual SpawnPool Pool()
    {
        this.LoadPool();
        return this.pool;
    }

    public virtual Transform Spawn(string objName, Transform parent)
    {
        return this.Pool().Spawn(objName, parent);
    }

    public virtual Transform Spawn(string objName, Vector3 pos, Quaternion rot)
    {
        return this.Pool().Spawn(objName, pos, rot);
    }

    public virtual Transform Spawn(string objName, Vector3 pos, Quaternion rot, Transform parent)
    {
        return this.Pool().Spawn(objName, pos, rot, parent);
    }

    public virtual void Despawm(Transform instance)
    {
        this.Pool().Despawn(instance);
    }
}
