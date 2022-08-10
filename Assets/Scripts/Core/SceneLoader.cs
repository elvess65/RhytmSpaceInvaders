using CoreFramework.SceneLoading;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace inGame.AbstractShooter.Core
{
    public class SceneLoader : CoreSceneLoader
    {
        public const string MAIN_SCENE_NAME = "MainScene";

        private const string m_TRANSITION_SCENE_NAME = "TransitionScene";
        private const string m_BOOT_SCENE_NAME = "BootScene";

        protected override string BootSceneName => m_BOOT_SCENE_NAME;

        public SceneLoader() : base()
        {
            LoadLevel(m_TRANSITION_SCENE_NAME);
        }

        protected override void SceneLoadCompleteHandler()
        {
            switch (m_CurrentLoadingLevel)
            {
                case MAIN_SCENE_NAME:
                    //Activate main scene
                    SceneManager.SetActiveScene(SceneManager.GetSceneByName(MAIN_SCENE_NAME));

                    //Fade out 
                    SceneLoadingManager.Instance.FadeOut();

                    //Initialization delay
                    SceneLoadingManager.Instance.StartCoroutine(InitializationCoroutine());

                    break;

                case m_TRANSITION_SCENE_NAME:
                    GameManager.Instance.Initialize();
                    break;
            }
        }

        protected override void SceneUnloadCompleteHandler() { }

        IEnumerator InitializationCoroutine()
        {
            yield return new WaitForSeconds(1);

            Debug.Log("Main scene is loaded");
        }
    }
}
