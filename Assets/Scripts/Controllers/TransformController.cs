using CoreFramework;
using inGame.AbstractShooter.Behaviours;
using inGame.AbstractShooter.Core;
using inGame.AbstractShooter.Models;
using UnityEngine;

namespace inGame.AbstractShooter.Controllers
{
    public class TransformController : BaseController
    {
        private UpdateModel m_updateModel;
        private CameraModel m_cameraModel;
        private GameplayModel m_gameplayModel;

        private Vector3 m_lowerScreenBound;
        private Vector3 m_upperScreenBound;

        private const float COLLISION_TRESHOLD = 0.3f;
        private const float OUT_OF_SCREEN_OFFSET_SCREEN_PERCENT = 0.1f;

        public TransformController(Dispatcher dispatcher) : base(dispatcher)
        { }

        public override void InitializeComplete()
        {
            base.InitializeComplete();

            m_updateModel = Dispatcher.GetModel<UpdateModel>();
            m_cameraModel = Dispatcher.GetModel<CameraModel>();

            m_gameplayModel = Dispatcher.GetModel<GameplayModel>();
            m_gameplayModel.OnGameLoopStarted += GameLoopStartedHandler;
            m_gameplayModel.OnGameLoopStopped += GameLoopStoppedHandler;
        }

        private void GameLoopStartedHandler()
        {
            m_updateModel.OnUpdate += UpdateHandler;

            float outOfScreenOffset = Screen.height * OUT_OF_SCREEN_OFFSET_SCREEN_PERCENT;
            
            //Upper screen bound + offset (Screen.height * some percent)
            Vector3 screenSpaceUpperBounds = new Vector3(Screen.width / 2f, Screen.height + outOfScreenOffset, m_cameraModel.MainCamera.nearClipPlane + Consts.NEAR_CLIP_PLANE_OFFSET);

            //Lower screen bound (literary lower screen bound)
            Vector3 screenSpaceLowerBounds = new Vector3(Screen.width / 2f, 0, m_cameraModel.MainCamera.nearClipPlane + Consts.NEAR_CLIP_PLANE_OFFSET);

            m_lowerScreenBound = m_cameraModel.MainCamera.ScreenToWorldPoint(screenSpaceLowerBounds);
            m_upperScreenBound = m_cameraModel.MainCamera.ScreenToWorldPoint(screenSpaceUpperBounds);
        }

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
                var current = m_gameplayModel.ActiveProjectiles[i];
                current.UpdateBehaviour(deltaTime);

                if (IsOutOfScreen(current))
                {
                    MonoBehaviour.Destroy(current.BehaviourGameObject);
                    m_gameplayModel.RemoveEntity(i--);

                    if (current.BehaviourType == BehaviourType.Enemy)
                    {
                        m_gameplayModel.OnDamagePlayer();
                    }
                }
            }
        }

        private bool IsOutOfScreen(iBehaviour behaviour)
        {
            if (behaviour.BehaviourType == BehaviourType.Friendly)
            {
                //If is upper than upper screen bound
                float distToEdgePoint = m_upperScreenBound.y - behaviour.BehaviourTransform.position.y;
                if (distToEdgePoint <= 0)
                {
                    return true;
                }
            }
            else
            {
                //If is lower thatn lower screen bound
                float distToEdgePoint = behaviour.BehaviourTransform.position.y - m_lowerScreenBound.y;
                if (distToEdgePoint <= 0)
                {
                    return true;
                }
            }

            return false;
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
                    if (current.BehaviourType != comparable.BehaviourType && sqrDst <= COLLISION_TRESHOLD)
                    {
                        m_gameplayModel.OnCollision(current, i, comparable, j);
                    }
                }
            }
        }
    }
}
