using UnityEngine;
using TMPro;
using NavMeshPlus.Components;

public class ForceDoor : MonoBehaviour
{
    public GameObject sceneObject;
    public TextMeshProUGUI customText;
    public SoundManager soundManager;
    public string custominteractiontext;
    public NavMeshSurface navMeshSurface;
    private bool isPlayerNear = false;


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            isPlayerNear = true;
            customText.text = custominteractiontext;
            ShowEButton();
        }
        if (collision.CompareTag("Bullet"))
        {
            // soundManager = ;
            navMeshSurface.UpdateNavMesh(navMeshSurface.navMeshData);
            Destroy(this.gameObject); // Destroy the bullet.
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
    }

    private void HideEButton()
    {
        sceneObject.SetActive(false);
        customText.text = "";
    }
}
