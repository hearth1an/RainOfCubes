using UnityEngine;

[RequireComponent(typeof(BoxCollider))]
public class Platform : MonoBehaviour
{
    private void Awake()
    {
        GetComponent<BoxCollider>().isTrigger = false;
    }    
}
