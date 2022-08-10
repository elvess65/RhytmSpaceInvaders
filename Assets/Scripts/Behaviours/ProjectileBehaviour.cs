using UnityEngine;

namespace inGame.AbstractShooter.Behaviours
{
    public class ProjectileBehaviour : MonoBehaviour, iBehaviour
    {
        public float MoveSpeed { get; set; }
        public Vector3 MoveDir { get; set; }
        public Transform BehaviourTransform => transform;
        public GameObject BehaviourGameObject => gameObject;
        

        public void UpdateBehaviour(float deltaTime)
        {
            transform.position += MoveDir * MoveSpeed * Time.deltaTime;
        }
    }
}
