public class CubesUIUpdater : StatsUIHandler<Cube>
{    
    private void OnEnable()
    {
        _spawner.ObjectSpawned += OnCubeSpawned;
    }

    private void OnDisable()
    {
        _spawner.ObjectSpawned -= OnCubeSpawned;
    }

    private void OnCubeSpawned(Cube cube)
    {
        UpdateUI();
    }
}
