using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
public class PoolManager : MonoBehaviour
{
    [SerializeField] List<Pool> _pools;

    public static object Pools { get;  set; }

    public void Initialize()
    {
        _pools.ForEach(pool => pool.Initialize());
    }

    public PoolObject GetItem(string objectName)
    {
       Pool pool = _pools.First(obj =>  obj.PoolName == objectName);

        return pool.GetItem();
    }
}
