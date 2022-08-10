using CoreFramework;
using inGame.AbstractShooter.Behaviours;
using inGame.AbstractShooter.Models;
using inGame.AbstractShooter.Spawn;
using UnityEngine;

namespace inGame.AbstractShooter.Controllers
{
    public class SpawnController : BaseController
    {
        private SpawnModel m_spawnModel;
        private CameraModel m_cameraModel;
        private UpdateModel m_updateModel;
        private GameplayModel m_gameplayModel;

        private iBehaviourSpawner m_enemySpawner;
        private iBehaviourSpawner m_friendlySpawner;

        public SpawnController(Dispatcher dispatcher) : base(dispatcher)
        { }

        public override void InitializeComplete()
        {
            base.InitializeComplete();

            m_cameraModel = Dispatcher.GetModel<CameraModel>();
            m_updateModel = Dispatcher.GetModel<UpdateModel>();
            m_spawnModel = Dispatcher.GetModel<SpawnModel>();

            m_gameplayModel = Dispatcher.GetModel<GameplayModel>();
            m_gameplayModel.OnGameLoopStarted += GameLoopStartedHandler;
            m_gameplayModel.OnGameLoopStopped += GameLoopStoppedHandler;
            m_gameplayModel.OnGameInitialized += GameInitializedHandler;
            m_gameplayModel.OnWantToSpawnFriendlyBehaviour += WantToSpawnFriendlyBehaviourHandler;
        }

        private void GameLoopStartedHandler() => m_updateModel.OnUpdate += UpdateHandler;

        private void GameLoopStoppedHandler() => m_updateModel.OnUpdate -= UpdateHandler;

        private void GameInitializedHandler()
        {
            float spawnDelay = 3;
            float initSpawnDelay = 1;
            m_enemySpawner = new EnemyBehaviourSpawner(m_cameraModel.MainCamera, spawnDelay, initSpawnDelay);
            m_enemySpawner.OnSpawn += BehaviourSpawnedHandler;

            m_friendlySpawner = new FriendlyBehaviourSpawner(m_cameraModel.MainCamera, m_spawnModel);
            m_friendlySpawner.OnSpawn += BehaviourSpawnedHandler;
        }

        private void WantToSpawnFriendlyBehaviourHandler(Vector3 screenPos, float dir)
        {
            m_spawnModel.SpawnScreenPos = screenPos;
            m_spawnModel.MoveDir = dir;

            m_friendlySpawner.SpawnBehaviour();
        }

        private void UpdateHandler(float deltaTime) => m_enemySpawner.HandleUpdate(deltaTime);

        private void BehaviourSpawnedHandler(iBehaviour entity) => m_gameplayModel.AddEntity(entity);
    }
}
