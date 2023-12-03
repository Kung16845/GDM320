using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Gun : MonoBehaviour
{
    public abstract int ammoInChamber { get; }
    public abstract bool isReloading { get; }
}
