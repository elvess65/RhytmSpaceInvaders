using UnityEngine;

namespace inGame.AbstractShooter.Behaviours
{
    public interface iBehaviour
    {
        Transform BehaviourTransform { get; }
        GameObject BehaviourGameObject { get; }

        void UpdateBehaviour(float deltaTime);
    }
}
