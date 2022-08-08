using CoreFramework;
using CoreFramework.Input;
using inGame.AbstractShooter.Behaviours;
using inGame.AbstractShooter.Models;
using UnityEngine;

namespace inGame.AbstractShooter.Controllers
{
    public class GameplayController : BaseController
    {
        private GameplayModel m_gameplayModel;
        private InputModel m_inputModel;

        public GameplayController(Dispatcher dispatcher) : base(dispatcher)
        { }

        public override void InitializeComplete()
        {
            base.InitializeComplete();

            m_gameplayModel = Dispatcher.GetModel<GameplayModel>();
            m_gameplayModel.OnCollision += CollisionHandler;

            m_inputModel = Dispatcher.GetModel<InputModel>();
        }

        public void StartGameLoop_TMP()
        {
            m_gameplayModel.OnGameLoopStarted();
            m_inputModel.OnTouch += TouchHandler;
        }

        public void StopGameLoop_TMP()
        {
            m_gameplayModel.OnGameLoopStopped();
            m_inputModel.OnTouch -= TouchHandler;
        }

        private void CollisionHandler(iBehaviour entityA, int indexA, iBehaviour entityB, int indexB)
        {
            Debug.Log("COLLISION");
            //TODO: Spawn effect

            MonoBehaviour.Destroy(entityA.BehaviourGameObject);
            MonoBehaviour.Destroy(entityB.BehaviourGameObject);

            m_gameplayModel.RemoveEntity(indexA);
            m_gameplayModel.RemoveEntity(indexB);
        }

        private void TouchHandler(Vector3 screenPos)
        {
            float dir = 1;
            if (screenPos.y > Screen.height / 3f)
                dir = -1;

            Debug.Log("spawn friendly projectile in dir " + dir);
        }
    }
}
