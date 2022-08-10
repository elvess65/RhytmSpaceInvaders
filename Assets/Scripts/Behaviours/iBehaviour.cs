using UnityEngine;

namespace inGame.AbstractShooter.Behaviours
{
    public interface iBehaviour
    {
        float MoveSpeed { get; set; }
        Vector3 MoveDir { get; set; }
        Transform BehaviourTransform { get; }
        GameObject BehaviourGameObject { get; }

        void UpdateBehaviour(float deltaTime);
    }
}
