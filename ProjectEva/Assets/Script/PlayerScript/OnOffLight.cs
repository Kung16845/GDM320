using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OnOffLight : MonoBehaviour
{
    
    public GameObject Light;
    public bool checkLight = false;
    public bool isHave = false;

    void Start() 
    {
        Light.SetActive(false);      
    }
    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyUp(KeyCode.F) && isHave == true)
        {
            if (checkLight)
            {
                Light.SetActive(false);
                checkLight = false;
            }
            else 
            {
                Light.SetActive(true);
                checkLight = true;
            } 
        }
    }
    
}
