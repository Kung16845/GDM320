using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryShotgun : MonoBehaviour
{
    public Shotgun shotgun;
    private void Awake() {
        shotgun = GameObject.FindAnyObjectByType<Shotgun>();
        shotgun.enable = true;
    }
    private void OnDestroy() {
    // ทำงานหลังจาก Object ถูกทำลาย
        shotgun.enable = false;
    }
}
