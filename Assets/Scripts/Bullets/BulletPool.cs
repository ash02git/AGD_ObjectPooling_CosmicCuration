using System.Collections.Generic;
using UnityEngine;

namespace CosmicCuration.Bullets
{
    public class BulletPool
    {
        private BulletView bulletView;
        private BulletScriptableObject bulletScriptableObject;
        private List<PooledBullet> pooledBullets = new List<PooledBullet>();

        public BulletPool(BulletView bulletView,BulletScriptableObject bulletScriptableObject)
        {
            this.bulletView = bulletView;
            this.bulletScriptableObject = bulletScriptableObject;
        }

        public BulletController GetBullet()
        {
            if(pooledBullets.Count > 0)
            {
                PooledBullet pooledBullet = pooledBullets.Find(item => item.isUsed==false);
                if(pooledBullet != null)
                {
                    //means we have found a bullet no being used
                    pooledBullet.isUsed = true;
                    return pooledBullet.Bullet;
                }
            }
            return CreateNewPooledBullet();
        }

        private BulletController CreateNewPooledBullet()
        {
            PooledBullet pooledBullet = new PooledBullet();
            pooledBullet.Bullet = new BulletController(bulletView,bulletScriptableObject);
            pooledBullet.isUsed=true;
            pooledBullets.Add(pooledBullet);

            return pooledBullet.Bullet;
        }

        public class PooledBullet
        {
            public bool isUsed;
            public BulletController Bullet;
        }
    }
}