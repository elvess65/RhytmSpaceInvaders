using CoreFramework;
using inGame.AbstractShooter.Behaviours;
using inGame.AbstractShooter.Core;
using inGame.AbstractShooter.Models;
using inGame.AbstractShooter.Spawn;
using RhytmFramework;
using UnityEngine;

namespace inGame.AbstractShooter.Controllers
{
    public class SpawnController : BaseController
    {
        private SpawnModel m_spawnModel;
        private CameraModel m_cameraModel;
        private GameplayModel m_gameplayModel;
        private RhytmController m_rhytmController;

        private iBehaviourSpawner m_enemySpawner;
        private iBehaviourSpawner m_friendlySpawner;

        public SpawnController(Dispatcher dispatcher) : base(dispatcher)
        { }

        public override void InitializeComplete()
        {
            base.InitializeComplete();

            m_rhytmController = Dispatcher.GetController<RhytmController>();

            m_cameraModel = Dispatcher.GetModel<CameraModel>();
            m_spawnModel = Dispatcher.GetModel<SpawnModel>();

            m_gameplayModel = Dispatcher.GetModel<GameplayModel>();
            m_gameplayModel.OnGameLoopStarted += GameLoopStartedHandler;
            m_gameplayModel.OnGameLoopStopped += GameLoopStoppedHandler;
            m_gameplayModel.OnGameInitialized += GameInitializedHandler;
            m_gameplayModel.OnWantToSpawnFriendlyBehaviour += WantToSpawnFriendlyBehaviourHandler;
        }

        private void GameLoopStartedHandler() => m_rhytmController.OnTick += TickHandler;

        private void GameLoopStoppedHandler() => m_rhytmController.OnTick -= TickHandler;

        private void GameInitializedHandler()
        {
            int spawnDelay = 3;
            int initSpawnDelay = 3;
            int minSpawnAmount = 1;
            int maxSpawnAmount = 2;
            m_enemySpawner = new EnemyBehaviourSpawner(m_cameraModel.MainCamera, Consts.NEAR_CLIP_PLANE_OFFSET, spawnDelay, initSpawnDelay, minSpawnAmount, maxSpawnAmount);
            m_enemySpawner.OnSpawn += BehaviourSpawnedHandler;

            m_friendlySpawner = new FriendlyBehaviourSpawner(m_cameraModel.MainCamera, Consts.NEAR_CLIP_PLANE_OFFSET, m_spawnModel);
            m_friendlySpawner.OnSpawn += BehaviourSpawnedHandler;
        }

        private void WantToSpawnFriendlyBehaviourHandler(Vector3 screenPos, float dir)
        {
            m_spawnModel.SpawnScreenPos = screenPos;
            m_spawnModel.MoveDir = dir;

            m_friendlySpawner.SpawnBehaviour();
        }

        private void TickHandler(int currentTick) => m_enemySpawner.HandleUpdate(currentTick);

        private void BehaviourSpawnedHandler(iBehaviour entity) => m_gameplayModel.AddEntity(entity);
    }
}
