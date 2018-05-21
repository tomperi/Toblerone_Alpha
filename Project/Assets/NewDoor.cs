using UnityEngine;
using UnityEngine.AI;

public class NewDoor : MonoBehaviour
{
    public bool doorOpen;
    public DoorColor color;

    NavMeshObstacle obstacle;
    private Collider collider;

    void Start()
    {
        obstacle = GetComponent<NavMeshObstacle>();
        collider = GetComponent<Collider>();
    }
    
    void OnTriggerEnter(Collider other)
    {
        Debug.Log("Enter");
        if (other.gameObject.tag == "Door" && color == other.GetComponentInParent<NewDoor>().color)
        {
            OpenDoors();
        }
    }

    void OnTriggerExit()
    {
        Debug.Log("Exit");
        CloseDoors();
    }

    public void OpenDoors()
    {
        doorOpen = true;
        obstacle.enabled = false;
    }

    public void CloseDoors()
    {
        doorOpen = false;
        obstacle.enabled = true;
    }
}
