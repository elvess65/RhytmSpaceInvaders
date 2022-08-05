using CoreFramework;
using UnityEngine;

namespace inGame.AbstractShooter.Controllers
{
    public class GameplayController : BaseController
    {
        private CameraModel m_cameraModel;

        public GameplayController(Dispatcher dispatcher) : base(dispatcher)
        { }

        public override void InitializeComplete()
        {
            base.InitializeComplete();

            m_cameraModel = Dispatcher.GetModel<CameraModel>();
        }

        public void CallSomething(Vector3 screenPos)
        {
            if (screenPos.y > Screen.height / 3f)
                return;

            Vector3 worldPos = m_cameraModel.MainCamera.ScreenToWorldPoint(new Vector3(screenPos.x, screenPos.y, m_cameraModel.MainCamera.nearClipPlane + 10));
            GameObject.CreatePrimitive(PrimitiveType.Sphere).transform.position = worldPos;
        }
    }
}
