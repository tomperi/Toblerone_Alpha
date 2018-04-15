using System;
using UnityEngine;
using UnityEngine.AI;

public class Door : MonoBehaviour
{

    public GameObject door;
    public bool[] colliders = new bool[2];

    Animator animator;
    NavMeshObstacle obstacle;
    bool doorOpen;

	void Start ()
	{
        doorOpen = false;
        animator = door.GetComponent<Animator>();
	    obstacle = door.GetComponent<NavMeshObstacle>();
	}

    void Update()
    {
        if (colliders[0] && colliders[1])
        {
            if (!doorOpen)
            {
                doorOpen = true;
                obstacle.enabled = false;
                Doors("Open");
            }
        }
        else if (doorOpen)
        {
            doorOpen = false;
            obstacle.enabled = true;
            Doors("Close");
        }


    }

    void Doors(string direction)
    {
        animator.SetTrigger(direction);
    }
}
