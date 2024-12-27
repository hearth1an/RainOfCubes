using System;
using System.Collections;
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
       if (collision.gameObject.TryGetComponent<Platform>(out Platform platform) && _isTriggered == false)
       {
            _isTriggered = true;
            ChangeColor();

            StartCoroutine(DestroyRoutine(GetLifetime()));
       }
    }

    private IEnumerator DestroyRoutine(float time)
    {
        float elapsedTime = 0;

        while (elapsedTime < time)
        {            
            elapsedTime += Time.deltaTime;
            
            yield return null;
        }

        CubeDestroyed?.Invoke(this);
    }
    
    private void ChangeColor()
    {
        _renderer.material.color = new Color(UnityEngine.Random.value, UnityEngine.Random.value, UnityEngine.Random.value);
    }    
}
