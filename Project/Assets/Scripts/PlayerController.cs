using UnityEngine;
using UnityEngine.AI;

public class PlayerController : MonoBehaviour {

	public Camera cam;
	public NavMeshAgent agent;

    NavMeshPath currentPath;

    private void Start()
    {
        currentPath = new NavMeshPath();
        agent.updateRotation = false;   
    }

    void Update () 
	{
		if (Input.GetMouseButtonDown (0)) 
		{	
			Ray ray = cam.ScreenPointToRay (Input.mousePosition);
			RaycastHit hit;

			if (Physics.Raycast (ray, out hit)) 
			{
				//agent.SetDestination (hit.point);
                //Debug.Log(hit.point);
                agent.CalculatePath(hit.point, currentPath);
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
