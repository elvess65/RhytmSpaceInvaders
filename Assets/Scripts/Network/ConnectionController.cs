using CoreFramework;
using CoreFramework.Network;
using inGame.AbstractShooter.Setup;
using UnityEngine;

namespace inGame.AbstractShooter.Network
{
    public class ConnectionController : CoreConnectionController<ConnectionSuccessResult>
    {
        public ConnectionController(iConnectionProvider<ConnectionSuccessResult> connectionProvider) : base(connectionProvider)
        { }

        protected override void Setup(ConnectionSuccessResult connectionResult)
        {
            //Implement me to setup data received on successfull connection

            Debug.Log($"ConnectionController: Setup {connectionResult.SerializedGameData}");
            
            //Example:
            IGameSetup gameSetup = new GameSetup(new DataSetup(connectionResult.SerializedGameData), new BehaviourSetup());
            gameSetup.Setup();
        }
    }
}
