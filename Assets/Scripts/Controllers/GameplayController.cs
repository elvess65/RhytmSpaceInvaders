using CoreFramework;
using CoreFramework.Input;
using inGame.AbstractShooter.Behaviours;
using inGame.AbstractShooter.Models;
using RhytmFramework;
using UnityEngine;

namespace inGame.AbstractShooter.Controllers
{
    public class GameplayController : BaseController
    {
        private RhytmInputProxy m_rhytmInputProxy;
        private GameplayModel m_gameplayModel;
        private InputModel m_inputModel;

        public GameplayController(Dispatcher dispatcher) : base(dispatcher)
        { }

        public override void InitializeComplete()
        {
            base.InitializeComplete();

            m_rhytmInputProxy = Dispatcher.GetController<RhytmInputProxy>();

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
            //TODO: Spawn effect

            MonoBehaviour.Destroy(entityA.BehaviourGameObject);
            MonoBehaviour.Destroy(entityB.BehaviourGameObject);

            m_gameplayModel.RemoveEntity(indexA);
            m_gameplayModel.RemoveEntity(indexB - 1);
        }

        private void TouchHandler(Vector3 screenPos)
        {
            if (!m_rhytmInputProxy.IsInputAllowed())
            {
                Debug.Log("CKICK NOT ALLOWED");
                return;
            }

            m_rhytmInputProxy.RegisterInput();

            float dir = screenPos.y > Screen.height * 0.25f || !m_rhytmInputProxy.IsInputTickValid() ? -1 : 1;

            m_gameplayModel.OnWantToSpawnFriendlyBehaviour.Invoke(screenPos, dir);
        }
    }
}
