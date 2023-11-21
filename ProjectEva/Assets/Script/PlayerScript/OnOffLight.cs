using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OnOffLight : MonoBehaviour
{
    
    public GameObject Light;
    public bool checkLight = false;
    public bool isopen = false;
    public bool canopen;

    void Start() 
    {
        Light.SetActive(false);
        canopen = false;      
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
    }
    
}
