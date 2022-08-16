using inGame.AbstractShooter.Behaviours;
using UnityEngine;

namespace inGame.AbstractShooter.Spawn
{
    public abstract class BehaviourSpawner : iBehaviourSpawner
    {
        public event System.Action<iBehaviour> OnSpawn;

        protected Camera m_mainCamera;
        protected float m_nearPlaneOffset;

        public BehaviourSpawner(Camera mainCamera, float nearPlaneOffset)
        {
            m_mainCamera = mainCamera;
            m_nearPlaneOffset = nearPlaneOffset;
        }

        public abstract void HandleUpdate(int currentTick);

        public void SpawnBehaviour()
        {
            var behaviour = InternalSpawnBehaviour();
            OnSpawn(behaviour);
        }


        protected abstract Vector3 GetSpawnWorldPosition();

        protected abstract Vector3 GetMoveDir();

        protected abstract iBehaviour InstantiateBehaviour();

        protected abstract float GetMoveSpeed();
        protected abstract BehaviourType GetBehaviourType();


        protected iBehaviour InternalSpawnBehaviour()
        {
            Vector3 worldPos = GetSpawnWorldPosition();
            Vector3 moveDir = GetMoveDir();
            float moveSpeed = GetMoveSpeed();
            BehaviourType behaviourType = GetBehaviourType();
            iBehaviour behaviour = InstantiateBehaviour();

            behaviour.BehaviourTransform.position = worldPos;
            behaviour.MoveDir = moveDir;
            behaviour.MoveSpeed = moveSpeed;
            behaviour.BehaviourType = behaviourType;

            return behaviour;
        }
    }
}
