using UnityEngine;

namespace inGame.AbstractShooter.Behaviours
{
    public class ProjectileComponent : MonoBehaviour, iBehaviour
    {
        public float speed;
        public Vector3 dir;

        public Transform BehaviourTransform => transform;
        public GameObject BehaviourGameObject => gameObject;

        public void UpdateBehaviour(float deltaTime)
        {
            transform.position += dir * speed * Time.deltaTime;
        }
    }
}
