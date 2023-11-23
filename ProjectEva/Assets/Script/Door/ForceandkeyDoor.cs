using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ForceandkeyDoor : MonoBehaviour
{
    private bool isPlayerNear = false;
    public int hasKeynumber;
    public InventoryPresentCharactor inventoryPresentCharactor;
    public GameObject sceneObject;
    public TextMeshProUGUI customText;
    public SoundManager soundManager;
    public string Keyforthisdoor;
    public int numberofkey;
    public string custominteractiontext;

    private void Start()
    {
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            isPlayerNear = true;
        }
        if (collision.CompareTag("Bullet"))
        {
            Destroy(this.gameObject); // Destroy the bullet.
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            isPlayerNear = false;
        }
    }

    private void Update()
    {
        if (isPlayerNear && Input.GetKeyDown(KeyCode.E) && hasKeynumber == numberofkey)
        {
            Destroy(this.gameObject);
        }
    }
}
