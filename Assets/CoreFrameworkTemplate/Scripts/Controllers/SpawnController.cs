using CoreFramework;
using inGame.AbstractShooter.Behaviours;
using inGame.AbstractShooter.Models;

namespace inGame.AbstractShooter.Controllers
{
    public class SpawnController : BaseController
    {
        private CameraModel m_cameraModel;
        private UpdateModel m_updateModel;
        private GameplayModel m_gameplayModel;

        private EnemySpawner m_enemySpawner;
     

        public SpawnController(Dispatcher dispatcher) : base(dispatcher)
        { }

        public override void InitializeComplete()
        {
            base.InitializeComplete();

            m_cameraModel = Dispatcher.GetModel<CameraModel>();
            m_updateModel = Dispatcher.GetModel<UpdateModel>();
            m_gameplayModel = Dispatcher.GetModel<GameplayModel>();
            m_gameplayModel.OnGameLoopStarted += GameLoopStartedHandler;
            m_gameplayModel.OnGameLoopStopped += GameLoopStoppedHandler;
            m_gameplayModel.OnGamePrepared += GamePreparedHandler;
        }

        private void GameLoopStartedHandler() => m_updateModel.OnUpdate += UpdateHandler;

        private void GameLoopStoppedHandler() => m_updateModel.OnUpdate -= UpdateHandler;

        private void GamePreparedHandler()
        {
            m_enemySpawner = new EnemySpawner(m_cameraModel.MainCamera, 3, 1);
            m_enemySpawner.OnSpawn += ProjectileSpawnHandler;
        }

        private void UpdateHandler(float deltaTime) => m_enemySpawner.HandleUpdate(deltaTime);

        private void ProjectileSpawnHandler(iBehaviour entity)
        {
            UnityEngine.Debug.Log("SPAWN");
            m_gameplayModel.AddEntity(entity);
        }
    }
}
