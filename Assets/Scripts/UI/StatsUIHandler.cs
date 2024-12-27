using UnityEngine;
using TMPro;

public abstract class StatsUIHandler<T> : MonoBehaviour where T : MonoBehaviour
{
    private const string Created = "Created: ";
    private const string Spawned = "Spawned: ";
    private const string Active = "Active: ";

    [SerializeField] protected TMP_Text TotalText;
    [SerializeField] protected TMP_Text SpawnedText;
    [SerializeField] protected TMP_Text ActiveText;
    [SerializeField] protected Spawner<T> Spawner;

    private void Awake()
    {
        Spawner.ObjectSpawned += OnObjectSpawned;
        UpdateUI();
    }

    private void OnDestroy()
    {
        Spawner.ObjectSpawned -= OnObjectSpawned;
    }

    private void UpdateTotal(int value)
    {
        TotalText.text = Created + value;
    }

    private void UpdateSpawned(int value)
    {
        SpawnedText.text = Spawned + value;
    }

    private void UpdateActive(int value)
    {
        ActiveText.text = Active + value;
    }

    private void UpdateUI()
    {
        UpdateTotal(Spawner.Created);
        UpdateSpawned(Spawner.Spawned);
        UpdateActive(Spawner.Active);
    }
    private void OnObjectSpawned(T obj)
    {
        UpdateUI();
    }
}
