using System;
using System.Collections.Generic;

namespace CosmicCuration.Utilities
{
    public class GenericObjectPool<T> where T : class
    {
        private List<PooledItem<T>> pooledItems = new List<PooledItem<T>>();

        protected T GetItem()
        {
            if(pooledItems.Count > 0)
            {
                PooledItem<T> item = pooledItems.Find(item => item.isUsed == false);

                if(item != null)
                {
                    item.isUsed = true;
                    return item.Item;
                }
            }
            return CreateNewPooledItem();
        }

        public void ReturnItem(T item)
        {
            PooledItem<T> returnedItem = pooledItems.Find(i=>i.Item.Equals(item));
            returnedItem.isUsed = false;    
        }

        private T CreateNewPooledItem()
        {
            PooledItem<T> newItem = new PooledItem<T>();
            newItem.Item = CreateNewItem();
            newItem.isUsed = true;
            pooledItems.Add(newItem);

            return newItem.Item;
        }

        protected virtual T CreateNewItem()
        {
            throw new NotImplementedException("Child class did not implement CreateNewItem()");
        }

        public class PooledItem<T>
        {
            public T Item;
            public bool isUsed;
        }
    }
}