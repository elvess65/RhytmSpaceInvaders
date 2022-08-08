using CoreFramework;
using inGame.AbstractShooter.Behaviours;
using System.Collections.Generic;

namespace inGame.AbstractShooter.Models
{
    public class GameplayModel : BaseModel
    {
        public System.Action OnGamePrepared;
        public System.Action OnGameLoopStarted;
        public System.Action OnGameLoopStopped;
        public System.Action<iBehaviour, int, iBehaviour, int> OnCollision;

        private List<iBehaviour> m_activeProjectiles;

        public List<iBehaviour> ActiveProjectiles => m_activeProjectiles;

        public override void Initialize()
        {
            base.Initialize();

            m_activeProjectiles = new List<iBehaviour>();
        }

        public void AddEntity(iBehaviour entity) => m_activeProjectiles.Add(entity);

        public void RemoveEntity(int index) => m_activeProjectiles.RemoveAt(index);
    }
}
