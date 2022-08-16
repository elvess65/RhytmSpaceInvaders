using inGame.AbstractShooter.Assets;
using inGame.AbstractShooter.Behaviours;
using inGame.AbstractShooter.Models;
using UnityEngine;

namespace inGame.AbstractShooter.Spawn
{
    public class FriendlyBehaviourSpawner : BehaviourSpawner
    {
        private SpawnModel m_spawnModel;
        private bool ShouldSpawnAsEnemy => m_spawnModel.MoveDir <= 0;

        public FriendlyBehaviourSpawner(Camera mainCamera, float nearClipPlaneOffset, SpawnModel spawnModel) : base(mainCamera, nearClipPlaneOffset)
        {
            m_spawnModel = spawnModel;
        }

        public override void HandleUpdate(int currentTick)
        { }

        protected override Vector3 GetSpawnWorldPosition()
        {
            Vector3 convertVec = new Vector3(m_spawnModel.SpawnScreenPos.x, m_spawnModel.SpawnScreenPos.y, m_mainCamera.nearClipPlane + m_nearPlaneOffset);
            return m_mainCamera.ScreenToWorldPoint(convertVec);
        }

        protected override Vector3 GetMoveDir() => Vector3.up * m_spawnModel.MoveDir;

        protected override iBehaviour InstantiateBehaviour()
        {
            ProjectileBehaviour source = ShouldSpawnAsEnemy ? 
                    AssetsManager.Instance.GetBehaviourAssets().EnemyBehaviour : 
                    AssetsManager.Instance.GetBehaviourAssets().FriendlyBehaviour;

            return AssetsManager.Instance.GetBehaviourAssets().InstantiatePrefab<ProjectileBehaviour>(source);
        }

        protected override float GetMoveSpeed() => 5f;

        protected override BehaviourType GetBehaviourType() => ShouldSpawnAsEnemy ? BehaviourType.Enemy : BehaviourType.Friendly;
    }
}
