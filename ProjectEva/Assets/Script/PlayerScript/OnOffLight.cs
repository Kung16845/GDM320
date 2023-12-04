using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OnOffLight : MonoBehaviour
{
    
    public GameObject Light;
    public GameObject PocketLight;
    public bool checkLight = false;
    public bool isopen = false;
    public bool canopen;
    public bool pocketcanopen;

    void Start() 
    {
        Light.SetActive(false);
        PocketLight.SetActive(false);
        canopen = false; 
        pocketcanopen = false;     
    }
    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyUp(KeyCode.F) && canopen)
        {
            if (checkLight)
            {
                Light.SetActive(false);
                checkLight = false;
                isopen = false;
            }
            else 
            {
                Light.SetActive(true);
                checkLight = true;
                isopen = true;
            } 
        }
        if(Input.GetKeyUp(KeyCode.F) && pocketcanopen)
        {
            if (checkLight)
            {
                PocketLight.SetActive(false);
                checkLight = false;
            }
            else 
            {
                PocketLight.SetActive(true);
                checkLight = true;
            } 
        }
    }
    
}
