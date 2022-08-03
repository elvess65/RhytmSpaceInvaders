using CoreFramework.SceneLoading;
using UnityEngine;

namespace CoreFramework.Examples
{
    public class MainSceneManager : MonoBehaviour
    {
        private GameStateMachine<GameState_Abstract> m_StateMachine;

        private void Start()
        {
            m_StateMachine = new GameStateMachine<GameState_Abstract>();
            m_StateMachine.AddState(new GameState_NoUI());
            m_StateMachine.AddState(new GameState_Normal());
            m_StateMachine.Initialize<GameState_NoUI>();
        }

        public void ChangeState()
        {
            m_StateMachine.ChangeState<GameState_Normal>();
        }

        public void ReloadLevel()
        {
            m_StateMachine.ChangeState<GameState_NoUI>();

            GameManager.Instance.SceneLoader.UnloadLevel(SceneLoader.MAIN_SCENE_NAME);
            GameManager.Instance.SceneLoader.LoadLevel(SceneLoader.MAIN_SCENE_NAME);
        }
    }
}
