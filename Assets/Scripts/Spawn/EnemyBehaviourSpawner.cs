using inGame.AbstractShooter.Assets;
using inGame.AbstractShooter.Behaviours;
using UnityEngine;

namespace inGame.AbstractShooter.Spawn
{
    public class EnemyBehaviourSpawner : BehaviourSpawner
    {
        private int m_ticksToNextSpawn;
        private int m_spawnDelay;
        private int m_initSpawnDelay;
        private int m_minSpawnAmount;
        private int m_maxSpawnAmount;

        public EnemyBehaviourSpawner(Camera mainCamera, float nearPlaneOffset, int spawnDelay, int initSpawnDelay, int minSpawnAmpunt, int maxSpawnAmount) : base(mainCamera, nearPlaneOffset)
        {
            m_spawnDelay = spawnDelay;
            m_initSpawnDelay = initSpawnDelay;
            m_minSpawnAmount = minSpawnAmpunt;
            m_maxSpawnAmount = maxSpawnAmount;

            m_ticksToNextSpawn = m_initSpawnDelay;
        }

        public override void HandleUpdate(int currentTick)
        {
            m_ticksToNextSpawn--;

            if (m_ticksToNextSpawn <= 0)
            {
                int spawnAmount = Random.Range(m_minSpawnAmount, m_maxSpawnAmount + 1);
                for (int i = 0; i < spawnAmount; i++)
                {
                    SpawnBehaviour();
                }

                m_ticksToNextSpawn = m_spawnDelay;
            }
        }

        protected override Vector3 GetSpawnWorldPosition()
        {
            Vector3 convertVec = new Vector3(Random.Range(0, Screen.width), Screen.height, m_mainCamera.nearClipPlane + m_nearPlaneOffset);
            return m_mainCamera.ScreenToWorldPoint(convertVec);
        }

        protected override Vector3 GetMoveDir() => Vector3.down;

        protected override iBehaviour InstantiateBehaviour() =>
            AssetsManager.Instance.GetBehaviourAssets().InstantiatePrefab<ProjectileBehaviour>(AssetsManager.Instance.GetBehaviourAssets().EnemyBehaviour);

        protected override float GetMoveSpeed() => 5f;

        protected override BehaviourType GetBehaviourType() => BehaviourType.Enemy;
    }
}
