using UnityEngine;
using TMPro;

public abstract class StatsUIHandler<T> : MonoBehaviour where T : MonoBehaviour
{
    private const string Created = "Created: ";
    private const string Spawned = "Spawned: ";
    private const string Active = "Active: ";

    [SerializeField] protected TMP_Text CreatedText;
    [SerializeField] protected TMP_Text SpawnedText;
    [SerializeField] protected TMP_Text ActiveText;
    [SerializeField] protected Spawner<T> Spawner;

    private void Awake()
    {
        UpdateUI();
    }

    private void UpdateTotal(int value)
    {
        CreatedText.text = Created + value;
    }

    private void UpdateSpawned(int value)
    {
        SpawnedText.text = Spawned + value;
    }

    private void UpdateActive(int value)
    {
        ActiveText.text = Active + value;
    }

    public virtual void UpdateUI()
    {
        UpdateTotal(Spawner.Created);
        UpdateSpawned(Spawner.Spawned);
        UpdateActive(Spawner.Active);
    }
}
