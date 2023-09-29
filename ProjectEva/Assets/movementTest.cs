using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
public class movementTest : MonoBehaviour
{
    public NavMeshAgent agent;
    public Transform points;
    // Start is called before the first frame update
    void Start()
    {
        agent.SetDestination(points.position);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
