using CoreFramework.Network;

namespace inGame.AbstractShooter.Network
{
    public class ConnectionSuccessResult : CoreConnectionSuccessResult
    {
        //Implement me to describe connection result

        public string SerializedGameData { get; }

        public ConnectionSuccessResult(string serializedGameData)
        {
            SerializedGameData = serializedGameData;
        }
    }
}
