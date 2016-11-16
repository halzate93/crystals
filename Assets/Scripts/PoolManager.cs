using System.Collections.Generic;
using UnityEngine;

public class PoolManager: MonoBehaviour
{
    [SerializeField]
    private Pool[] pools;
    private Dictionary<string, Pool> map;

    public static PoolManager Instance
    {
        get; private set;
    }

    Pool this [GameObject prefab]
    {
        get
        {
            Pool pool;
            if (!map.TryGetValue(prefab.name, out pool))
                Debug.LogWarning ("There's no pool with that prefab name");
            return pool;
        }
    }

    private void Awake ()
    {
        if (Instance == null)
        {
            Instance = this;
            AddPools ();
        }
        else
            Destroy (gameObject);
    }

    public GameObject Instantiate (GameObject prefab, Vector3 position, Quaternion rotation)
    {
        Pool pool = this[prefab];
        if (pool == null) 
            return GameObject.Instantiate (prefab, position, rotation) as GameObject;
        else
            return pool.Allocate (position, rotation);
    }

    public void Release (GameObject instance)
    {
        Pool pool = this[instance];
        if (pool == null)
            Destroy (instance);
        else
            pool.Release (instance);
    }

    private void AddPools ()
    {
        map = new Dictionary<string, Pool> ();
        foreach (Pool pool in pools)
                map.Add (pool.Prefab.name, pool);
    }
}