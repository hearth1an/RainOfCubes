public class BombsStatsUpdater : StatsUIHandler<Bomb>
{   
    private void OnEnable()
    {
        Spawner.ObjectSpawned += OnBombSpawned;
        Spawner.ObjectDeactivated += OnBombSpawned;
    }

    private void OnDisable()
    {
        Spawner.ObjectSpawned -= OnBombSpawned;
        Spawner.ObjectDeactivated -= OnBombSpawned;
    }

    private void OnBombSpawned(Bomb bomb)
    {
        UpdateUI();
    }
}
