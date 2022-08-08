using CoreFramework;
using inGame.AbstractShooter.Models;

namespace inGame.AbstractShooter.Controllers
{
    public class TransformController : BaseController
    {
        private UpdateModel m_updateModel;
        private GameplayModel m_gameplayModel;

        private const float COLLISION_TRESHOLD = 0.3f;

        public TransformController(Dispatcher dispatcher) : base(dispatcher)
        { }

        public override void InitializeComplete()
        {
            base.InitializeComplete();

            m_updateModel = Dispatcher.GetModel<UpdateModel>();

            m_gameplayModel = Dispatcher.GetModel<GameplayModel>();
            m_gameplayModel.OnGameLoopStarted += GameLoopStartedHandler;
            m_gameplayModel.OnGameLoopStopped += GameLoopStoppedHandler;
        }

        private void GameLoopStartedHandler() => m_updateModel.OnUpdate += UpdateHandler;

        private void GameLoopStoppedHandler() => m_updateModel.OnUpdate -= UpdateHandler;

        private void UpdateHandler(float deltaTime)
        {
            UpdatePositions(deltaTime);
            HandleCollisions();
        }

        private void UpdatePositions(float deltaTime)
        {
            for (int i = 0; i < m_gameplayModel.ActiveProjectiles.Count; i++)
            {
                m_gameplayModel.ActiveProjectiles[i].UpdateBehaviour(deltaTime);
            }
        }

        private void HandleCollisions()
        {
            for (int i = 0; i < m_gameplayModel.ActiveProjectiles.Count; i++)
            {
                var current = m_gameplayModel.ActiveProjectiles[i];

                for (int j = 0; j < m_gameplayModel.ActiveProjectiles.Count; j++)
                {
                    var comparable = m_gameplayModel.ActiveProjectiles[j];

                    if (current == comparable)
                        continue;

                    float sqrDst = (current.BehaviourTransform.position - comparable.BehaviourTransform.position).sqrMagnitude;
                    if (sqrDst <= COLLISION_TRESHOLD)
                    {
                        m_gameplayModel.OnCollision(current, i, comparable, j);
                    }
                }
            }
        }
    }
}
