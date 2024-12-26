using System;
using UnityEngine;

[RequireComponent(typeof(Renderer), typeof(Collider))]
public class Cube : Item
{
    private Color _baseColor = Color.red;
    private bool _isTriggered = false;
    private Renderer _renderer;
    
    public event Action<Cube> CubeDestroyed;

    private void Awake()
    {
        _renderer = GetComponent<Renderer>();
        _renderer.material.color = _baseColor;       
    }

    private void OnCollisionEnter(Collision collision)
    {
       if (collision.gameObject.GetComponent<Platform>() && _isTriggered == false)
       {
            _isTriggered = true;
            ChangeColor();
            
            Invoke(nameof(TriggerDestroy), GetLifetime());
       }
    }

    private void TriggerDestroy()
    {
        CubeDestroyed?.Invoke(this);
    }

    private void ChangeColor()
    {
        _renderer.material.color = new Color(UnityEngine.Random.value, UnityEngine.Random.value, UnityEngine.Random.value);
    }

    public int GetDestroyDelay()
    {
        int minNumber = 2;
        int maxNumber = 5;

        return UnityEngine.Random.Range(minNumber, maxNumber);
    }
}
