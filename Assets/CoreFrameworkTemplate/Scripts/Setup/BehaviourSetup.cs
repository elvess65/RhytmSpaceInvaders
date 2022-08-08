using CoreFramework;
using inGame.AbstractShooter.Controllers;
using inGame.AbstractShooter.Models;

namespace inGame.AbstractShooter.Setup
{
    /// <summary>
    /// Setup для боя
    /// </summary>
    public class BehaviourSetup : IGameSetup
    {
        public void Setup()
        {
            Dispatcher dispatcher = Dispatcher.Instance;

            // Controllers
            dispatcher.CreateController<GameplayController>();
            dispatcher.CreateController<SpawnController>();
            dispatcher.CreateController<TransformController>();

            // Models
            dispatcher.CreateModel<GameplayModel>();
        }
    }
}
