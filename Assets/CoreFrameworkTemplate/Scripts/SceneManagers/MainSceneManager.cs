using UnityEngine;
using CoreFramework;
using inGame.AbstractShooter.Controllers;
using inGame.AbstractShooter.StateMachine;
using inGame.AbstractShooter.Models;

namespace inGame.AbstractShooter.SceneControllers
{
    public class MainSceneManager : MonoBehaviour
    {
        public RectTransform TMP_RectTransform;

        private GameStateMachine<GameState_Abstract> m_StateMachine;

        private Dispatcher m_dispatcher;

        private void Awake()
        {
            m_dispatcher = Dispatcher.Instance;
        }

        private void Start()
        {
            m_StateMachine = new GameStateMachine<GameState_Abstract>();
            m_StateMachine.AddState(new GameState_NoUI());
            m_StateMachine.AddState(new GameState_Normal());
            m_StateMachine.Initialize<GameState_NoUI>();

            m_dispatcher.GetModel<CameraModel>().MainCamera = Camera.main;
            m_dispatcher.GetModel<GameplayModel>().OnGamePrepared();

            var sizeDelta = TMP_RectTransform.sizeDelta;
            sizeDelta = new Vector2(sizeDelta.x, Screen.height * 0.25f);
            TMP_RectTransform.sizeDelta = sizeDelta;
        }

        //TMP Called from button handler
        public void Initialize()
        {
            m_dispatcher.GetController<GameplayController>().StartGameLoop_TMP();
        }
    }
}