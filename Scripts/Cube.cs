using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Renderer), typeof(Collider))]
public class Cube : MonoBehaviour
{
    private Color _baseColor = Color.red;
    private bool _isTriggered = false;

    private Renderer _renderer;

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
            Invoke(nameof(TriggerDestroy), GetDestroyDelay());
       }
    }

    private void TriggerDestroy()
    {
        Destroy(gameObject);
    }

    private void ChangeColor()
    {
        _renderer.material.color = new Color(Random.value, Random.value, Random.value);
    }

    private int GetDestroyDelay()
    {
        int minNumber = 2;
        int maxNumber = 5;

        return Random.Range(minNumber, maxNumber);
    }
}
