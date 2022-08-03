using CoreFramework.Network;
using UnityEngine;

namespace CoreFramework.Examples
{
    public class GameManager : Singleton<GameManager>
    {
        public SceneLoader SceneLoader { get; private set; }
        public bool SimulateConnectionError = false;

        public void Initialize()
        {
            ConnectionController connectionController = new ConnectionController(new ConnectionProviderLocal(SimulateConnectionError));
            connectionController.OnConnectionSuccess += ConnectionResultSuccess;
            connectionController.OnConnectionError += ConnectionResultError;
            connectionController.Connect();
        }

        private void Start()
        {
            DontDestroyOnLoad(gameObject);

            SceneLoader = new SceneLoader();
        }

        private void ConnectionResultSuccess()
        {
            SceneLoader.LoadLevel(SceneLoader.MAIN_SCENE_NAME);
        }

        private void ConnectionResultError(int errorCode)
        {
            Debug.Log($"Error connection. Code {errorCode}");
        }
    }
}
