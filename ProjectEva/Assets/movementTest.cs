using UnityEngine;
using UnityEngine.AI;

public class MovementTest : MonoBehaviour
{
    public NavMeshAgent agent;
    public Transform walktotunal;
    public Transform tunaltowalk;
    public bool isUsingTunnel = false;
    public AudioClip tunnelSound;  // Sound to play when entering the tunnel
    public AudioSource audioSource;  // Reference to an AudioSource component
    
    private float originalSpeed;  // Store the original speed of the agent

    private void Start()
    {
        // Store the original speed of the agent
        originalSpeed = agent.speed;

        // Set the initial destination to walk to tunnel
        SetDestination(walktotunal.position);
    }

    private void Update()
    {
        if (agent.remainingDistance <= agent.stoppingDistance && !agent.pathPending)
        {
            // Toggle the isUsingTunnel flag each time the agent reaches its destination
            isUsingTunnel = !isUsingTunnel;

            // Set the new destination based on the isUsingTunnel flag
            SetDestination(isUsingTunnel ? tunaltowalk.position : walktotunal.position);

            // Adjust the speed of the agent
            if (isUsingTunnel)
            {
                agent.speed *= 2;  // Double the speed when entering the tunnel
                PlaySound(tunnelSound);  // Play the tunnel sound
            }
            else
            {
                agent.speed = originalSpeed;  // Reset the speed when exiting the tunnel
            }
        }
    }

    private void SetDestination(Vector3 destination)
    {
        // Determine the area mask based on the isUsingTunnel flag
        string areaName = isUsingTunnel ? "Tunnel" : "Walkable";
        int areaLayer = NavMesh.GetAreaFromName(areaName);
        if (areaLayer == -1)
        {
            Debug.LogError("Navigation Area with name '" + areaName + "' not found.");
            return;
        }
        int areaMask = 1 << areaLayer;

        // Update the agent's area mask and set the new destination
        agent.areaMask = areaMask;
        agent.SetDestination(destination);
    }

    private void PlaySound(AudioClip clip)
    {
        // Play the specified sound
        audioSource.clip = clip;
        audioSource.Play();
    }
}
