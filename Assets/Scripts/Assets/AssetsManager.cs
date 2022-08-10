using CoreFramework;
using UnityEngine;

namespace inGame.AbstractShooter.Assets
{
    public class AssetsManager : Singleton<AssetsManager>
    {
        [SerializeField] private BehaviourAssets m_behaviourAssets = null;

        public BehaviourAssets GetBehaviourAssets() => m_behaviourAssets;

        private void Start()
        {
            DontDestroyOnLoad(gameObject);

            Initialize();
        }    

        protected virtual void Initialize()
        {
            m_behaviourAssets.Initialize();
        }
    }
}
