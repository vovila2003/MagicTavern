using System.Collections.Generic;
using UnityEngine;

namespace Tavern.Common.Pool
{
    public sealed class Pool<T> where T : Component
    {
        private readonly Transform _parent;
        private readonly T _prefab;
        private readonly Queue<T> _pool;
        private readonly PoolLimit _limit;

        public Pool(T prefab, int startPoolLength, PoolLimit limit, Transform parent)
        {
            _parent = parent;
            _prefab = prefab;
            _pool = new Queue<T>();
            _limit = limit;
            
            FillPool(startPoolLength);
        }

        public bool TrySpawn(out T instance)
        {
            instance =  null;
            if (_pool.TryDequeue(out T go))
            {
                instance = go;
                return true;
            }

            if (_limit == PoolLimit.Limited)
            {
                return false;
            }
            
            Create(_prefab);

            if (!_pool.TryDequeue(out T newGo)) return false;
            
            instance = newGo;
            return true;
        }

        public void Unspawn(T item)
        {
            item.transform.SetParent(_parent);
            _pool.Enqueue(item);
        }

        private void FillPool(int startPoolLength)
        {
            for (var i = 0; i < startPoolLength; i++)
            {
                Create(_prefab);
            }
        }

        private void Create(T prefab)
        {
            T go = Object.Instantiate(prefab, _parent);
            _pool.Enqueue(go);
        }
    }
}