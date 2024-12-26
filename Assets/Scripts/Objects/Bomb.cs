using System;
using System.Collections;
using UnityEngine;

public class Bomb : Item
{   
    private float _lifeTime;
    private float _targetAlpha = 0f;
    private float _explosionForce = 5;

    private int _radius = 5;
    
    private MeshRenderer _renderer;    

    public event Action<Bomb> BombDestroyed;

    private void Awake()
    {
        _renderer = GetComponent<MeshRenderer>();
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

        Color startColor = _renderer.material.color;
        Color targetColor = new Color(startColor.r, startColor.g, startColor.b, _targetAlpha);

        while (elapsedTime < _lifeTime)
        {
            elapsedTime += Time.deltaTime;

            _renderer.material.color = Color.Lerp(startColor, targetColor, elapsedTime / _lifeTime);

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
            if (hit.TryGetComponent<Rigidbody>(out Rigidbody rb))
            {                
                rb.AddExplosionForce(_explosionForce, transform.position, _radius, upwardsModifier, ForceMode.Impulse);
            }
        }

        BombDestroyed?.Invoke(this);
    }
}
