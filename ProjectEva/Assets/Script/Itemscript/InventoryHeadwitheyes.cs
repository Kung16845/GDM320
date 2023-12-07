using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryHeadwitheyes : MonoBehaviour
{
      public Headlessquest headless;
      void Awake()
      {
        headless = FindObjectOfType<Headlessquest>();
        headless.Headwitheyeequip = true;
      }
      private void OnDestroy()
    {
        headless.Headwitheyeequip = false;
    }
}
