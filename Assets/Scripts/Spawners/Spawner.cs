using System;
using UnityEngine;
using UnityEngine.Pool;

public abstract class Spawner<T> : MonoBehaviour where T : MonoBehaviour
{    
    [SerializeField] protected T Prefab;
    
    private ObjectPool<T> _pool;    

    public int Total { get; private set; } = 0;
    public int Spawned { get; private set; } = 0;
    public int Active { get; private set; } = 0;
    public float RepeatRate { get; private set; } = 1f;

    public event Action<T> ObjectSpawned;
    public event Action<T> ObjectDeactivated;

    private void Awake()
    {
        _pool = new ObjectPool<T>(
            createFunc: CreateObject,
            actionOnGet: ActivateObject,
            actionOnRelease: HandleReleased
        );
    }

    public T CreateObject()
    {
        Active++;
        Spawned++;

        return Instantiate(Prefab);
    }

    public abstract void ActivateObject(T obj);

    public abstract void DeactivateObject(T obj);

    public void AddSpawned(T obj)
    {
        ObjectSpawned?.Invoke(obj);
        Total++;
    }

    public virtual void HandleReleased(T obj)
    {
        DeactivateObject(obj);
        ObjectDeactivated?.Invoke(obj);

        Active--;
    }

    protected virtual void ReleaseObject(T obj)
    {
        _pool.Release(obj);
    }

    protected virtual void GetObject()
    {                
        _pool.Get();        
    }
}
