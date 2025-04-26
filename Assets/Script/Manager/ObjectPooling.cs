using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;


public class ObjectPooling : Singleton<ObjectPooling>
{
    public ObjectData[] objsInfor;
    private Dictionary<E_PoolName, Queue<GameObject>> _pools = new Dictionary<E_PoolName, Queue<GameObject>>();
    private E_PoolName[] _gunPool;

    [SerializeField] private bool _runWhenStart;
    protected override void Awake()
    {
        base.Awake();
        foreach (var pool in objsInfor)
        {
            if(_pools.ContainsKey(pool.poolName))
            {
                _pools.Add(pool.poolName, new());
            }
        }
        CreateObjectInPool(Vector3.zero);
    }
    public void CreateObjectInPool(Vector3 spawnPoint)
    {
        foreach (ObjectData obj in objsInfor)
        {
            Queue<GameObject> objects = new();
            for (int i = 0; i < obj.numberAvail; i++)
            {
                GameObject ob = Instantiate(obj.pref, spawnPoint, Quaternion.identity);
                ob.SetActive(false);
                objects.Enqueue(ob);
            }
            _pools.Add(obj.poolName, objects);
        }
    }
    public GameObject GetObjectFromPool(E_PoolName name, Vector3 pos)
    {
        foreach (var pool in _pools)
        {
            if (pool.Key == name)
            {
                if(_pools[name].Count > 0)
                {
                    GameObject obj = _pools[name].Dequeue();
                    obj.transform.position = pos;
                    obj.SetActive(true);

                    return obj;
                }

            }
        }
        GameObject ob = Instantiate(FindPoolName(name).pref, pos, Quaternion.identity);
        return ob;
    }

    public void ReturnPool(GameObject obj,E_PoolName name)
    {
        if(!FindPoolName(name))
        {
            return;
        }
        obj.SetActive(false);
        _pools[name].Enqueue(obj);
    }
    private ObjectData FindPoolName(E_PoolName name)
    {
        return objsInfor.FirstOrDefault(obj => obj.poolName == name);
    }
}

