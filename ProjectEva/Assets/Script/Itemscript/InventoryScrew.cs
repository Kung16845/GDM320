using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryScrew : MonoBehaviour
{
   public Eyeballquest eyeballquest;
      void Awake()
      {
        eyeballquest = FindObjectOfType<Eyeballquest>();
        eyeballquest.Screwequip = true;
      }
      private void OnDestroy()
    {
        eyeballquest.Screwequip = false;
    }
}
