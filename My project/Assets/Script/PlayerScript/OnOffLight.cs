using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OnOffLight : MonoBehaviour
{
    
    public GameObject Light;
    public GameObject player;
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
    private void OnTriggerEnter2D(Collider2D other) 
    {
        
        if(other.gameObject.tag == "Light")
        {
            isHave = true;
            // Instantiate(other.transform,player.transform.parent,true);
            Destroy(other.gameObject);
        }
    }
}
