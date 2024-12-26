using UnityEngine;

[RequireComponent(typeof(BoxCollider))]
public class SpawnPlatform : MonoBehaviour
{
    [SerializeField] private Platform _platform;

    private float _additionalHeight = 5f;

    private void Awake()
    {
        GetComponent<BoxCollider>().isTrigger = true;

        transform.localScale = _platform.transform.localScale;
        transform.position = _platform.transform.position;

        Vector3 position = gameObject.transform.position;
        position.y += _additionalHeight; 
        transform.position = position;
    }
        
}
