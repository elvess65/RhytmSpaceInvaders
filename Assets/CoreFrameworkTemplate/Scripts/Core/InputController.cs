using UnityEngine;

namespace CoreFramework.Input
{
    public class InputController : CoreInputController
    {
        public InputController(Dispatcher dispatcher) : base(dispatcher)
        { }

        protected override void HandleSpecialInput()
        {
            if (UnityEngine.Input.GetKeyDown(KeyCode.Space))
            {
                m_InputModel.KeyDown(KeyCode.Space);
            }
        }
    }
}
