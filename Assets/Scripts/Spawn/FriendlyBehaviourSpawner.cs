using inGame.AbstractShooter.Assets;
using inGame.AbstractShooter.Behaviours;
using inGame.AbstractShooter.Models;
using UnityEngine;

namespace inGame.AbstractShooter.Spawn
{
    public class FriendlyBehaviourSpawner : BehaviourSpawner
    {
        private SpawnModel m_spawnModel;

        public FriendlyBehaviourSpawner(Camera mainCamera, SpawnModel spawnModel) : base(mainCamera)
        {
            m_spawnModel = spawnModel;
        }

        public override void HandleUpdate(float deltaTime)
        { }

        protected override Vector3 GetSpawnWorldPosition()
        {
            Vector3 convertVec = new Vector3(m_spawnModel.SpawnScreenPos.x, m_spawnModel.SpawnScreenPos.y, m_mainCamera.nearClipPlane + NEAR_PLANE_OFFSET);
            return m_mainCamera.ScreenToWorldPoint(convertVec);
        }

        protected override Vector3 GetMoveDir() => Vector3.up * m_spawnModel.MoveDir;

        protected override iBehaviour InstantiateBehaviour()
        {
            ProjectileBehaviour source = null;

            if (m_spawnModel.MoveDir > 0)
                source = AssetsManager.Instance.GetBehaviourAssets().FriendlyBehaviour;
            else
                source = AssetsManager.Instance.GetBehaviourAssets().EnemyBehaviour;

            return AssetsManager.Instance.GetBehaviourAssets().InstantiatePrefab<ProjectileBehaviour>(source);
        }

        protected override float GetMoveSpeed() => 5f;
    }
}
