public class BombSpawner : Spawner<Bomb>
{    
    public void CreateBomb(Cube cube)
    {        
        Bomb bomb = CreateObject();
        bomb.transform.position = cube.transform.position;
        ActivateObject(bomb);
        bomb.BombDestroyed += HandleDestroyed;
    }

    public override void ActivateObject(Bomb bomb)
    {        
        AddSpawned(bomb);
    }

    public override void DeactivateObject(Bomb bomb)
    {
        bomb.BombDestroyed -= HandleDestroyed;
    }
}
