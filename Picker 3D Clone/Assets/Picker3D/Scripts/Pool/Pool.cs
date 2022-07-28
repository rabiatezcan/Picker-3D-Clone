using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
public class Pool : MonoBehaviour
{
    [SerializeField] private PoolObject _item;
    [SerializeField] private string _poolName;
    [SerializeField] private int _initialPoolSize;

    private List<PoolObject> _itemPool = new List<PoolObject>();

    public string PoolName => _poolName;
    public void Initialize()
    {
        GenerateObject(_initialPoolSize);

    }
    public PoolObject GetItem()
    {
        if (_itemPool.All(i => i.IsTaken))
            GenerateObject(CONSTANTS.POOL_INCREMENT_AMOUNT);

        return _itemPool.First(i => !i.IsTaken);
    }

    private void GenerateObject(int amount)
    {
        for (int i = 0; i < amount; i++)
        {
            PoolObject pooledObject = Instantiate(_item);
            pooledObject.Dismiss();
            pooledObject.transform.parent = transform;
            _itemPool.Add(pooledObject);
        }
    }
}
