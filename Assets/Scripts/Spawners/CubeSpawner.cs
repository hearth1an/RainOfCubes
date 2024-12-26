using UnityEngine;

public class CubeSpawner : Spawner<Cube>
{
    [SerializeField] private BombSpawner _bombSpawner;
    [SerializeField] private SpawnPlatform _spawnPlatform;

    private Vector3 _platformPosition => _spawnPlatform.transform.position;
    private Vector3 _platformScale => _spawnPlatform.transform.localScale;

    private void Start()
    {
        float time = 0f;

        InvokeRepeating(nameof(GetObject), time, RepeatRate);
    }

    private void OnEnable()
    {
        ObjectSpawned += OnCubeSpawned;
    }

    private void OnDisable()
    {
        ObjectSpawned -= OnCubeSpawned;
    }

    public override void ActivateObject(Cube cube)
    {
        cube.transform.position = GetRandomPosition();
        AddSpawned(cube);
        cube.CubeDestroyed += HandleDestroyed;
    }

    public override void DeactivateObject(Cube cube)
    {
        cube.CubeDestroyed -= _bombSpawner.CreateBomb;
        cube.CubeDestroyed -= HandleDestroyed;
    }

    private void OnCubeSpawned(Cube cube)
    {
       cube.CubeDestroyed += _bombSpawner.CreateBomb;
    }

    private Vector3 GetRandomPosition()
    {
        int halfDivide = 2;

        float halfX = _platformScale.x / halfDivide;
        float halfZ = _platformScale.z / halfDivide;

        float xPosition = _platformPosition.x + UnityEngine.Random.Range(-halfX, halfX);
        float zPosition = _platformPosition.z + UnityEngine.Random.Range(-halfZ, halfZ);

        return new Vector3(xPosition, _platformPosition.y, zPosition);
    }
}