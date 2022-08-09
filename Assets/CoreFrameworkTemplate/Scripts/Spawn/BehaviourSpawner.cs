using inGame.AbstractShooter.Behaviours;
using UnityEngine;

namespace inGame.AbstractShooter.Spawn
{
    public abstract class BehaviourSpawner : iBehaviourSpawner
    {
        public event System.Action<iBehaviour> OnSpawn;

        protected Camera m_mainCamera;

        protected const float NEAR_PLANE_OFFSET = 10;

        public BehaviourSpawner(Camera mainCamera)
        {
            m_mainCamera = mainCamera;
        }

        public abstract void HandleUpdate(float deltaTime);

        public void SpawnBehaviour()
        {
            var behaviour = InternalSpawnBehaviour();
            OnSpawn(behaviour);
        }


        protected abstract Vector3 GetSpawnWorldPosition();

        protected abstract Vector3 GetMoveDir();

        protected abstract GameObject GetSource();

        protected abstract float GetMoveSpeed();


        protected iBehaviour InternalSpawnBehaviour()
        {
            Vector3 worldPos = GetSpawnWorldPosition();
            Vector3 moveDir = GetMoveDir();
            float moveSpeed = GetMoveSpeed();
            GameObject source = GetSource();

            source.transform.position = worldPos;

            var projectile = source.AddComponent<ProjectileComponent>();
            projectile.dir = moveDir;
            projectile.speed = moveSpeed;

            return projectile;
        }
    }
}
