using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using NavMeshPlus.Components;

public class OpenfromthisSideDoor : MonoBehaviour
{
    private bool isPlayerNear = false;
    public GameObject sceneObject;
    public TextMeshProUGUI customText;
    public SoundManager soundManager;
    public NavMeshSurface navMeshSurface;
    public string custominteractiontext;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            isPlayerNear = true;
            ShowEButton();
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            HideEButton();
            isPlayerNear = false;
        }
    }
    private void ShowEButton()
    {
        sceneObject.SetActive(true);
        customText.text = custominteractiontext;
    }

    private void HideEButton()
    {
        sceneObject.SetActive(false);
        customText.text = "";
    }
    private void Update()
    {
        if (isPlayerNear && Input.GetKeyDown(KeyCode.E))
        {
            soundManager.PlaySound("Dooropen");
            navMeshSurface.UpdateNavMesh(navMeshSurface.navMeshData);
            Destroy(this.gameObject);
        }
    }
}
