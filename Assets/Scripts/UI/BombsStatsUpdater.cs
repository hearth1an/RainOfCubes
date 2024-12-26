public class BombsStatsUpdater : StatsUIHandler<Bomb>
{   
    private void OnEnable()
    {
        _spawner.ObjectSpawned += OnBombSpawned;
    }

    private void OnDisable()
    {
        _spawner.ObjectSpawned -= OnBombSpawned;
    }

    private void OnBombSpawned(Bomb bomb)
    {
        UpdateUI();
    }
}
