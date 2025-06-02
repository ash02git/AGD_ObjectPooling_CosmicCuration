using System.Collections.Generic;

namespace CosmicCuration.Enemy
{
    public class EnemyPool
    {
        private EnemyView enemyView;
        private EnemyScriptableObject enemyScriptableObject;
        private List<PooledEnemy> pooledEnemies = new List<PooledEnemy>();

        public EnemyPool(EnemyView enemyView, EnemyScriptableObject enemyScriptableObject)
        {
            this.enemyView = enemyView;
            this.enemyScriptableObject = enemyScriptableObject;
        }

        public EnemyController GetEnemy()
        {
            if(pooledEnemies.Count > 0)
            {
                PooledEnemy pooledEnemy = pooledEnemies.Find(item=>item.isUsed == false);

                if(pooledEnemy != null)
                {
                    pooledEnemy.isUsed = true;
                    return pooledEnemy.Enemy;
                }
            }
            return CreateNewPooledEnemy();
        }

        public void ReturnEnemyToPool(EnemyController returnedEnemy)
        {
            PooledEnemy pooledEnemy = pooledEnemies.Find(item=>item.Enemy.Equals(returnedEnemy));
            pooledEnemy.isUsed = false;
        }

        private EnemyController CreateNewPooledEnemy()
        {
            PooledEnemy pooledEnemy = new PooledEnemy();
            pooledEnemy.Enemy = new EnemyController(enemyView, enemyScriptableObject.enemyData);
            pooledEnemy.isUsed = true;
            pooledEnemies.Add(pooledEnemy);

            return pooledEnemy.Enemy;
        }

        public class PooledEnemy
        {
            public bool isUsed;
            public EnemyController Enemy;
        }
    }
}