using inGame.AbstractShooter.Behaviours;

namespace inGame.AbstractShooter.Spawn
{
    public interface iBehaviourSpawner
    {
        event System.Action<iBehaviour> OnSpawn;

        void HandleUpdate(float deltaTime);
        void SpawnBehaviour();
    }
}
