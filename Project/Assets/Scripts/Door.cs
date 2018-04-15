using System;
using UnityEngine;
using UnityEngine.AI;

public class Door : MonoBehaviour
{

    public GameObject door;
    public bool[] colliders = new bool[2];
    private LayerMask openDoorLayerMask;
    private LayerMask closedDoorLayerMask;

    Animator animator;
    NavMeshObstacle obstacle;
    bool doorOpen;


    void Start()
    {
        doorOpen = false;
        animator = door.GetComponent<Animator>();
        obstacle = door.GetComponent<NavMeshObstacle>();

        openDoorLayerMask = LayerMask.NameToLayer("OpenDoors");
        closedDoorLayerMask = LayerMask.NameToLayer("ClosedDoors");
        setAllChildLayers(closedDoorLayerMask);
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
                setAllChildLayers(openDoorLayerMask);
            }
        }
        else if (doorOpen)
        {
            doorOpen = false;
            obstacle.enabled = true;
            Doors("Close");
            setAllChildLayers(closedDoorLayerMask);
        }


    }

    void Doors(string direction)
    {
        animator.SetTrigger(direction);
    }

    private void setAllChildLayers(int mask)
    {
        gameObject.layer = mask;

        foreach (Transform child in gameObject.transform)
        {
            child.gameObject.layer = mask;
        }

    }
}
