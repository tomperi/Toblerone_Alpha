using UnityEngine;
using UnityEngine.AI;

public class BasicEnemy : MonoBehaviour
{
    public float lookRadius = 5f;

    public Transform target;
    private NavMeshAgent agent;
    private NavMeshPath path;
	
    // Use this for initialization
	void Start ()
	{
	    agent = GetComponent<NavMeshAgent>();
    }
	
	// Update is called once per frame
	void Update ()
	{
	    float distance = Vector3.Distance(target.position, transform.position);

	    if (distance <= lookRadius)
	    {
            GoToPosition(target.position);
	        if (path.status == NavMeshPathStatus.PathComplete)
	        {
	            agent.SetPath(path);
	        }
	    }
	}

    public void GoToPosition(Vector3 destination)
    {
        agent.CalculatePath(destination, path);
    }

    public void StopNavAgent()
    {
        agent.enabled = false;
    }

    public void StartNavAgent()
    {
        agent.enabled = true;
    }

    public void StopAtPlace()
    {
        GoToPosition(transform.position);
    }

    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, lookRadius);
    }
}
