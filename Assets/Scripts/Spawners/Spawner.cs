using System;
using UnityEngine;
using UnityEngine.Pool;

public abstract class Spawner<T> : MonoBehaviour where T : MonoBehaviour
{    
    [SerializeField] protected T _prefab;
    
    private ObjectPool<T> _pool;    

    public int Total { get; private set; } = 0;
    public int Spawned { get; private set; } = 0;
    public int Active { get; private set; } = 0;
    public float RepeatRate { get; private set; } = 1f;

    public event Action<T> ObjectSpawned;

    private void Awake()
    {
        _pool = new ObjectPool<T>(
            createFunc: CreateObject,
            actionOnGet: ActivateObject,
            actionOnDestroy: HandleDestroyed
        );
    }

    public T CreateObject()
    {
        Active++;
        Spawned++;

        return Instantiate(_prefab);
    }

    public abstract void ActivateObject(T obj);

    public abstract void DeactivateObject(T obj);

    public void AddSpawned(T obj)
    {
        ObjectSpawned?.Invoke(obj);
        Total++;
    }

    public virtual void HandleDestroyed(T obj)
    {
        DeactivateObject(obj);

        Destroy(obj.gameObject);
        Active--;
    }

    protected virtual void GetObject()
    {                
        _pool.Get();        
    }
}
