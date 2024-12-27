using UnityEngine;

public class BombSpawner : Spawner<Bomb>
{
    private Vector3 _cubePosition;

    public void CreateBomb(Cube cube)
    {
        _cubePosition = cube.transform.position;
        GetObject();               
    }

    public override void ActivateObject(Bomb bomb)
    {
        bomb.transform.position = _cubePosition;
        FixVelocity(bomb);        
        bomb.gameObject.SetActive(true);
        bomb.BombDestroyed += ReleaseObject;       
    }

    public override void DeactivateObject(Bomb bomb)
    {
        bomb.gameObject.SetActive(false);
        bomb.BombDestroyed -= ReleaseObject;
    }
}
