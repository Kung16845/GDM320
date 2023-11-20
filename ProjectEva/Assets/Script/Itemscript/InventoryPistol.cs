using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryPistol : MonoBehaviour
{
    public Pistol pistol;
    private void Awake() {
        pistol = GameObject.FindAnyObjectByType<Pistol>();
        pistol.enable = true;
    }
    private void OnDestroy() {
    // ทำงานหลังจาก Object ถูกทำลาย
    pistol.enable = false;
}
}
