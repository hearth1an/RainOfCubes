using System;
using UnityEngine;
using UnityEngine.Pool;

public abstract class Spawner<T> : MonoBehaviour where T : MonoBehaviour
{    
    [SerializeField] protected T Prefab;
    
    private ObjectPool<T> _pool;    

    public int Created { get; private set; } = 0;
    public int Spawned => GetSpawned();
    public int Active => GetActive();
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
        T obj = Instantiate(Prefab);
        ObjectSpawned?.Invoke(obj);

        return obj;
    }

    public abstract void ActivateObject(T obj);

    public abstract void DeactivateObject(T obj);

    public virtual void HandleReleased(T obj)
    {
        DeactivateObject(obj);
        ObjectDeactivated?.Invoke(obj);
    }

    private int GetSpawned()
    {
        int minValue = 0;

        if (_pool == null)
            return minValue;

        return _pool.CountAll;
    }

    private int GetActive()
    {
        int minValue = 0;

        if (_pool == null)
            return minValue;

        return _pool.CountActive;
    }

    protected virtual void FixVelocity(T obj)
    {
        if (obj.TryGetComponent<Rigidbody>(out Rigidbody rigidbody))
        {
            rigidbody.velocity = Vector3.zero;
        }
    }

    protected virtual void ReleaseObject(T obj)
    {
        _pool.Release(obj);
    }

    protected virtual void GetObject()
    {
        Created++;
        _pool.Get();        
    }
}
