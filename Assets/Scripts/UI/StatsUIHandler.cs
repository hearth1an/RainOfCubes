using UnityEngine;
using TMPro;

public abstract class StatsUIHandler<T> : MonoBehaviour where T : MonoBehaviour
{
    [SerializeField] protected TMP_Text _totalText;
    [SerializeField] protected TMP_Text _spawnedText;
    [SerializeField] protected TMP_Text _activeText;

    [SerializeField] protected Spawner<T> _spawner;

    private const string Total = "Total: ";
    private const string Spawned = "Spawned: ";
    private const string Active = "Active: ";

    private void Awake()
    {
        UpdateUI();
    }

    private void UpdateTotal(int value)
    {
        _totalText.text = Total + value;
    }

    private void UpdateSpawned(int value)
    {
        _spawnedText.text = Spawned + value;
    }

    private void UpdateActive(int value)
    {
        _activeText.text = Active + value;
    }

    public virtual void UpdateUI()
    {
        UpdateTotal(_spawner.Total);
        UpdateSpawned(_spawner.Spawned);
        UpdateActive(_spawner.Active);
    }
}
