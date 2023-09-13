using UnityEngine;
using UnityEngine.PlayerLoop;

public class FreezeZRotation : MonoBehaviour
{
    private Quaternion initialRotation; // ค่าการหมุนเริ่มต้น

    void Start()
    {
        initialRotation = transform.rotation;
    }
    private void Update()
    {
        transform.rotation = initialRotation;
    }
}
