using UnityEngine;

public class FreezeZRotation : MonoBehaviour
{   

    void Update()
    {
        transform.rotation = Quaternion.Euler(transform.rotation.eulerAngles.x, transform.rotation.eulerAngles.y, 0.0f);
        
    }
}
