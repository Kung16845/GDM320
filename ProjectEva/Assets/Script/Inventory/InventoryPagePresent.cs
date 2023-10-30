using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class InventoryPagePresent : MonoBehaviour
{   
    public int pageInventory;
    public List<GameObject> allPageInventort;
    // Update is called once per frame
    private void Start() 
    {
        
    }
    public void BackPage()
    {
        allPageInventort.ElementAt<GameObject>(pageInventory).SetActive(false);

        pageInventory -= 1;

        allPageInventort.ElementAt<GameObject>(pageInventory).SetActive(true);
    }
    public void NextPage()
    {
        allPageInventort.ElementAt<GameObject>(pageInventory).SetActive(false);

        pageInventory += 1;

        allPageInventort.ElementAt<GameObject>(pageInventory).SetActive(true);
    }
}
