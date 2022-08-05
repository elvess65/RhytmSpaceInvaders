using CoreFramework;

namespace inGame.AbstractShooter.Setup
{
    /// <summary>
    /// Setup требующий заполнения из БД
    /// </summary>
    public class DataSetup : IGameSetup
    {
        private string m_SerializedGameData;

        public DataSetup(string serializedGameData)
        {
            m_SerializedGameData = serializedGameData;
        }

        public void Setup()
        {
            Dispatcher dispatcher = Dispatcher.Instance;

            ICustomSerializer serializer = new JsonSerializer();

            UnityEngine.Debug.Log($"DataSetup. Deserialize {m_SerializedGameData}");

            //Example:
            //dispatcher.CreateModel(serializer.Deserialize<GameDataModel>(m_SerializedGameData));
        }
    }
}