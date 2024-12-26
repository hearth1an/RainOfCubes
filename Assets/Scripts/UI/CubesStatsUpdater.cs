public class CubesUIUpdater : StatsUIHandler<Cube>
{    
    private void OnEnable()
    {
        Spawner.ObjectSpawned += OnCubeSpawned;
        //Spawner.ObjectDeactivated += OnCubeSpawned;
    }

    private void OnDisable()
    {
        Spawner.ObjectSpawned -= OnCubeSpawned;
        //Spawner.ObjectDeactivated -= OnCubeSpawned;
    }

    private void OnCubeSpawned(Cube cube)
    {
        UpdateUI();
    }
}
