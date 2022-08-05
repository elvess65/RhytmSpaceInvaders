using CoreFramework;
using CoreFramework.Input;
using inGame.AbstractShooter.Controllers;

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
            dispatcher.CreateController<InputController>();
            dispatcher.CreateController<GameplayController>();

            // Models
            dispatcher.CreateModel<InputModel>();
        }
    }
}
