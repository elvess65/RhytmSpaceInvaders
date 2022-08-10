using UnityEngine;
using CoreFramework;
using inGame.AbstractShooter.Controllers;
using inGame.AbstractShooter.StateMachine;
using inGame.AbstractShooter.Models;
using RhytmFramework;
using CoreFramework.Utils;
using RhytmFighter.UI.Widget;

namespace inGame.AbstractShooter.SceneControllers
{
    public class MainSceneManager : MonoBehaviour
    {
        public RectTransform TMP_RectTransform;
        public Metronome Metronome;
        public UIWidget_Tick Widget_Tick;
        public int BPM = 60;
        public float InputPrecision = 0.25f;

        private GameStateMachine<GameState_Abstract> m_StateMachine;

        private Dispatcher m_dispatcher;
        private GameplayController m_gameplayController;
        private RhytmController m_rhytmController;
        private RhytmInputProxy m_rhytmInputProxy;

        private GameplayModel m_gameplayModel;
        private InterpolationData<float> m_LerpData;

        private void Awake()
        {
            m_dispatcher = Dispatcher.Instance;
        }

        private void Start()
        {
            //State Machine
            m_StateMachine = new GameStateMachine<GameState_Abstract>();
            m_StateMachine.AddState(new GameState_NoUI());
            m_StateMachine.AddState(new GameState_Normal());
            m_StateMachine.Initialize<GameState_NoUI>();

            //Models
            m_dispatcher.GetModel<CameraModel>().MainCamera = Camera.main;
            m_gameplayModel = m_dispatcher.GetModel<GameplayModel>();
            m_gameplayModel.OnGameLoopStarted += GameLoopStartedHandler;
            m_gameplayModel.OnGameLoopStopped += GameLoopStoppedHandler;

            //Controllers
            m_gameplayController = m_dispatcher.GetController<GameplayController>();
            m_rhytmController = m_dispatcher.GetController<RhytmController>();
            m_rhytmController.SetBPM(BPM);
            m_rhytmInputProxy = m_dispatcher.GetController<RhytmInputProxy>();
            m_rhytmInputProxy.SetInputPrecious(InputPrecision);

            //Other
            var sizeDelta = TMP_RectTransform.sizeDelta;
            sizeDelta = new Vector2(sizeDelta.x, Screen.height * 0.25f);
            TMP_RectTransform.sizeDelta = sizeDelta;

            Metronome.bpm = BPM;
            Widget_Tick.Initialize((float)m_rhytmController.TickDurationSeconds / 8f, m_rhytmInputProxy, m_rhytmController);

            //Main loop
            m_gameplayModel.OnGameInitialized();
        }

        //TMP Called from button handler
        public void Initialize()
        {
            m_gameplayController.StartGameLoop_TMP();
        }

        private void GameLoopStartedHandler()
        {
            Metronome.StartMetronome();

            m_rhytmController.OnTick += OnTickHandler;
            m_rhytmController.OnEventProcessingTick += OnEventProcessingTickHandler;
            m_rhytmController.StartTicking();

            m_dispatcher.GetModel<UpdateModel>().OnUpdate += UpdateHandler_TMP;
        }

        private void GameLoopStoppedHandler()
        {
            m_dispatcher.GetModel<UpdateModel>().OnUpdate -= UpdateHandler_TMP;

            m_rhytmController.OnTick -= OnTickHandler;
            m_rhytmController.OnEventProcessingTick -= OnEventProcessingTickHandler;
            m_rhytmController.StopTicking();

            Metronome.StopMetronome();
        }

        void OnTickHandler(int tick)
        {
         
        }

        void OnEventProcessingTickHandler(int tick)
        {
            Widget_Tick.PlayTickAnimation();
            Widget_Tick.PlayArrowsAnimation();
        }

        void UpdateHandler_TMP(float deltaTime)
        {
            Widget_Tick.PerformUpdate(deltaTime);
        }
    }
}