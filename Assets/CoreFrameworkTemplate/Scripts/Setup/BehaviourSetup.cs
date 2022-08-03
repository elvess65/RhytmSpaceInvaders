using CoreFramework.Input;

namespace CoreFramework.Examples
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

            // Models
            dispatcher.CreateModel<InputModel>();
        }
    }
}
