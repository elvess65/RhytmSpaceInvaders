using inGame.AbstractShooter.Assets;
using inGame.AbstractShooter.Behaviours;
using UnityEngine;

namespace inGame.AbstractShooter.Spawn
{
    public class EnemyBehaviourSpawner : BehaviourSpawner
    {
        private float m_spawnTimer;
        private float m_spawnDelay;
        private float m_initSpawnDelay;

        public EnemyBehaviourSpawner(Camera mainCamera, float spawnDelay, float initSpawnDelay) : base(mainCamera)
        {
            m_spawnDelay = spawnDelay;
            m_initSpawnDelay = initSpawnDelay;

            m_spawnTimer = m_initSpawnDelay;
        }

        public override void HandleUpdate(float deltaTime)
        {
            m_spawnTimer -= deltaTime;

            if (m_spawnTimer <= 0)
            {
                SpawnBehaviour();

                m_spawnTimer = m_spawnDelay;
            }
        }

        protected override Vector3 GetSpawnWorldPosition()
        {
            Vector3 convertVec = new Vector3(Random.Range(0, Screen.width), Screen.height, m_mainCamera.nearClipPlane + NEAR_PLANE_OFFSET);
            return m_mainCamera.ScreenToWorldPoint(convertVec);
        }

        protected override Vector3 GetMoveDir() => Vector3.down;

        protected override iBehaviour InstantiateBehaviour() =>
            AssetsManager.Instance.GetBehaviourAssets().InstantiatePrefab<ProjectileBehaviour>(AssetsManager.Instance.GetBehaviourAssets().EnemyBehaviour);

        protected override float GetMoveSpeed() => 5f;
    }
}
