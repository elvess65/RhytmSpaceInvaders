using CoreFramework.StateMachine;

namespace CoreFramework.Examples
{
    public class GameStateMachine<T> : AbstractStateMachine<T> where T: GameState_Abstract
    {
    }
}
