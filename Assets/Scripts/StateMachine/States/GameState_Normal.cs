namespace inGame.AbstractShooter.StateMachine
{
    public class GameState_Normal : GameState_Abstract
    {
        public GameState_Normal() : base()
        {
        }

        public override void EnterState()
        {
            base.EnterState();

            UnityEngine.Debug.Log("EnterState GameState_Normal");
        }

        public override void ExitState()
        {
            base.ExitState();

            UnityEngine.Debug.Log("ExitState GameState_Normal");
        }
    }
}
