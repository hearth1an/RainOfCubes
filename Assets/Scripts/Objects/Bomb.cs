using System;
using System.Collections;
using UnityEngine;

public class Bomb : Item
{    
    private float _lifeTime;
    private float _targetAlpha = 0f;
    private float _startAlpha = 1f;
    private float _explosionForce = 5;
    private Color _startingColor;  

    private int _radius = 5;
    
    private MeshRenderer _renderer;    

    public event Action<Bomb> BombDestroyed;

    private void OnEnable()
    {
        _renderer = GetComponent<MeshRenderer>();
        _startingColor = new Color(_renderer.material.color.r, _renderer.material.color.g, _renderer.material.color.b, _startAlpha);        
        Initialize();
    }    

    public void Initialize()
    {
        _lifeTime = GetLifetime();
        StartCoroutine(TriggerExplosion());
    }

    private IEnumerator TriggerExplosion()
    {
        float elapsedTime = 0;
        
        Color targetColor = new Color(_startingColor.r, _startingColor.g, _startingColor.b, _targetAlpha);

        while (elapsedTime < _lifeTime)
        {
            elapsedTime += Time.deltaTime;

            _renderer.material.color = Color.Lerp(_startingColor, targetColor, elapsedTime / _lifeTime);

            yield return null;
        }

        _renderer.material.color = targetColor;

        Explode();
    }

    private void Explode()
    {
        float upwardsModifier = 0.1f;

        Collider[] colliders = Physics.OverlapSphere(transform.position, _radius);

        foreach (Collider hit in colliders)
        {            
            if (hit.TryGetComponent<Rigidbody>(out Rigidbody rigidbody))
            {                
                rigidbody.AddExplosionForce(_explosionForce, transform.position, _radius, upwardsModifier, ForceMode.Impulse);
            }
        }

        BombDestroyed?.Invoke(this);
    }
}
