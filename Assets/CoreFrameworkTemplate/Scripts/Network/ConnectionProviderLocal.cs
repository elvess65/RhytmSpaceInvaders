using CoreFramework.Network;
using UnityEngine;

namespace inGame.AbstractShooter.Network
{
    public class ConnectionProviderLocal : CoreConnectionProviderLocal<ConnectionSuccessResult, DBLocal>
    {
        public ConnectionProviderLocal(bool simulateConnectionError) : base(simulateConnectionError)
        { }

        protected override ConnectionSuccessResult BuildSuccessResult()
        {
            //Implement me to create a new ConnectionSuccessResult
            var connectionSuccessResult = new ConnectionSuccessResult(JsonUtility.ToJson(m_localDataBase));

            Debug.Log($"ConnectionProviderLocal: BuildSuccessResult {connectionSuccessResult.SerializedGameData}");

            return connectionSuccessResult;
        }
    }
}
