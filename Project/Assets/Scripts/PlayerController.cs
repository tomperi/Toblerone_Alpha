using UnityEngine;
using UnityEngine.AI;

public class PlayerController : MonoBehaviour {

	public Camera cam;
	public NavMeshAgent agent;
    public FrameManager frameManager;

    private NavMeshPath currentPath;
    public LayerMask layerMask;

    private void Start()
    {
        currentPath = new NavMeshPath();

        // Prevents the agent from rotating 
        agent.updateRotation = false;  
        
        // Take the layer mask from the frame manager
    }

    void Update () 
	{
		if (Input.GetMouseButtonDown (0)) 
		{	
			Ray ray = cam.ScreenPointToRay (Input.mousePosition);
			RaycastHit hit;

			if (Physics.Raycast (ray, out hit, 10000f, layerMask)) 
			{
				//agent.SetDestination (hit.point);
                //Debug.Log(hit.point);
                agent.CalculatePath(hit.point, currentPath);
                //Debug.Log(currentPath.status);
                if (currentPath.status == NavMeshPathStatus.PathComplete)
                {
                    agent.SetPath(currentPath);
                }

			}
		}

        if (Input.GetKeyDown("z"))
        {
            // Stop the player in place
            agent.SetDestination(transform.position);
        }
	}
}
