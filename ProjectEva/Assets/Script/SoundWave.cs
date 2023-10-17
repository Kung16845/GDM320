using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundWave : MonoBehaviour
{
    public Collider2D SoundRadis;
    public bool isReducing;
    // Start is called before the first frame update
    void Start()
    {
        SoundRadis = this.GetComponent<CircleCollider2D>();
    }
    private void Update()
    {
        if (this.gameObject.activeInHierarchy == true && !isReducing)
        {   
            isReducing = true;
            StartCoroutine(ReduceRadiusSoundWave());
        }
    }
    IEnumerator ReduceRadiusSoundWave()
    {
        // SoundRadis.GetComponent<CircleCollider2D>().radius -= 1;
        yield return new WaitForSeconds(1.0f);
        Debug.Log("ReduceRadiusSound is Complete");
        if (SoundRadis.GetComponent<CircleCollider2D>().radius > 0.1)
        {
            isReducing = false;
            this.gameObject.SetActive(false);
            // StartCoroutine(ReduceRadiusSoundWave());
        }
            
        // else
        // {
        //     SoundRadis.GetComponent<CircleCollider2D>().radius = 16;
        //     isReducing = false;
        //     this.gameObject.SetActive(false);
        // }
    }
}
