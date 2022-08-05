using CoreFramework;
using CoreFramework.Input;
using inGame.AbstractShooter.Controllers;
using inGame.AbstractShooter.StateMachine;
using UnityEngine;

namespace inGame.AbstractShooter.SceneControllers
{
    public class MainSceneManager : MonoBehaviour
    {
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

            var gameplayController = m_dispatcher.GetController<GameplayController>();

            m_dispatcher.GetModel<CameraModel>().MainCamera = Camera.main;
            m_dispatcher.GetModel<UpdateModel>().OnUpdate += UpdateHandler;


        }

        float timer = 0;

        private void UpdateHandler(float deltaTime)
        {
            timer += deltaTime;

            if (timer > 3)
            {
                Vector3 worldPos = m_dispatcher.GetModel<CameraModel>().MainCamera.ScreenToWorldPoint(new Vector3(Random.Range(0, 1080), 1900f, m_dispatcher.GetModel<CameraModel>().MainCamera.nearClipPlane + 10));
                GameObject.CreatePrimitive(PrimitiveType.Sphere).transform.position = worldPos;
                timer = 0;
            }
        }

        private void TouchHandler(Vector3 touchScreenPos)
        {
            Debug.LogError($"Touch {touchScreenPos}");
            m_dispatcher.GetController<GameplayController>().CallSomething(touchScreenPos);
        }

        public void Initialize()
        {
            Debug.Log("Initialize game");

            m_dispatcher.GetModel<InputModel>().OnTouch += TouchHandler;
        }
    }
}