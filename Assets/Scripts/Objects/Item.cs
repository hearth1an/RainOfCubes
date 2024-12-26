using UnityEngine;

public abstract class Item: MonoBehaviour
{
    public int MinLifeTime { get; private set; } = 2;
    public int MaxLifeTime { get; private set; } = 5;

    public virtual int GetLifetime()
    {
        return UnityEngine.Random.Range(MinLifeTime, MaxLifeTime);
    }
}
