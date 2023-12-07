using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventorywine : MonoBehaviour
{
    public Headlessquest headlessquest;
    void Awake()
      {
        headlessquest = FindObjectOfType<Headlessquest>();
        headlessquest.iswineequip = true;
      }
      private void OnDestroy()
    {
        headlessquest.iswineequip = false;
    }
}
