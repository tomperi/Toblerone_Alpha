using UnityEngine;
using UnityEngine.AI;

public class SpriteController : MonoBehaviour
{
    private NavMeshAgent agent;

    private Animator animator;

	// Use this for initialization
	void Start ()
	{
	    agent = GetComponentInParent<NavMeshAgent>();
	    animator = GetComponent<Animator>();
	}
	
	// Update is called once per frame
	void Update () {
	    if (agent.velocity.x == 0)
	    {
	        animator.SetInteger("Direction", 0);
	    }
	    else if (agent.velocity.x > .5f)
	    {
	        animator.SetInteger("Direction", 2);
	    }
	    else if (agent.velocity.x < -.5f)
	    {
	        animator.SetInteger("Direction", 1);
	    }
	    else if (agent.velocity.z > .5f)
	    {
	        // Move Up
	    }
	    else if (agent.velocity.z < 0f)
	    {
	        // Move Down
	    }
    }

    void LateUpdate()
    {

    }
}
