using System.Collections;
using UnityEngine;

public class CubeSpawner : Spawner<Cube>
{
    [SerializeField] private BombSpawner _bombSpawner;
    [SerializeField] private SpawnPlatform _spawnPlatform;

    private WaitForSeconds _delay;
    private Vector3 _platformPosition => _spawnPlatform.transform.position;
    private Vector3 _platformScale => _spawnPlatform.transform.localScale;

    private void Start()
    {
        _delay = new WaitForSeconds(RepeatRate);
        StartCoroutine(GetObjectRoutine());
    }

    public override void ActivateObject(Cube cube)
    {        
        FixVelocity(cube);
        
        cube.gameObject.SetActive(true);        

        cube.transform.position = GetRandomPosition();
        cube.CubeDestroyed += _bombSpawner.CreateBomb;
        cube.CubeDestroyed += ReleaseObject;        
    }

    public override void DeactivateObject(Cube cube)
    {
        cube.gameObject.SetActive(false);
        cube.CubeDestroyed -= _bombSpawner.CreateBomb;
        cube.CubeDestroyed -= ReleaseObject;                
    }

    private IEnumerator GetObjectRoutine()
    {
        while (enabled)
        {
            GetObject();
            yield return _delay;
        }
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