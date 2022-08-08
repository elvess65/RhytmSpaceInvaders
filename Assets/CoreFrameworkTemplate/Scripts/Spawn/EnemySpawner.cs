using inGame.AbstractShooter.Behaviours;
using UnityEngine;

public class EnemySpawner 
{
    public event System.Action<iBehaviour> OnSpawn;

    private float m_spawnTimer;
    private float m_spawnDelay;
    private float m_initSpawnDelay;
    private Camera m_mainCamera;

    private const float NEAR_PLANE_OFFSET = 10;

    public EnemySpawner(Camera mainCamera, float spawnDelay, float initSpawnDelay)
    {
        m_mainCamera = mainCamera;

        m_spawnDelay = spawnDelay;
        m_initSpawnDelay = initSpawnDelay;

        m_spawnTimer = m_initSpawnDelay;
    }

    public void HandleUpdate(float deltaTime)
    {
        m_spawnTimer -= deltaTime;

        if (m_spawnTimer <= 0)
        {
            OnSpawn(Spawn(GetSpawnWorldPosition()));

            m_spawnTimer = m_spawnDelay; 
        }
    }

    private Vector3 GetSpawnWorldPosition() => m_mainCamera.ScreenToWorldPoint(new Vector3(Random.Range(0, 1080), 1900f, m_mainCamera.nearClipPlane + NEAR_PLANE_OFFSET));

    private iBehaviour Spawn(Vector3 worldPos)
    {
        var go = GameObject.CreatePrimitive(PrimitiveType.Sphere);
        go.transform.position = worldPos;

        var projectile = go.AddComponent<ProjectileComponent>();
        projectile.dir = Vector3.down;
        projectile.speed = 5f;

        return projectile;
    }
}
