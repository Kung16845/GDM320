using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CamaraPlayer : MonoBehaviour
{
    public GameObject player;
    public Vector3 offset;

    void LateUpdate()
    {
        // ตำแหน่งของกล้อง = ตำแหน่งของผู้เล่น + offset
        transform.position = player.transform.position + offset;
    }
    
}
