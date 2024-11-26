using UnityEngine;

[RequireComponent(typeof(BoxCollider))]
public class SpawnPlatform : MonoBehaviour
{
    [SerializeField] private Platform _platform;

    private float _height = 5f;

    private void Awake()
    {
        GetComponent<BoxCollider>().isTrigger = true;

        gameObject.transform.localScale = _platform.transform.localScale;
        gameObject.transform.position = _platform.transform.position;

        Vector3 position = gameObject.transform.position;
        position.y += _height; 
        gameObject.transform.position = position;
    }
        
}
