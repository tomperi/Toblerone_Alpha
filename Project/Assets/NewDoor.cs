using UnityEngine;
using UnityEngine.AI;

public class NewDoor : MonoBehaviour
{
    public bool doorOpen;
    public DoorColor color;

    NavMeshObstacle obstacle;

    void Start()
    {
        obstacle = GetComponent<NavMeshObstacle>();
    }
    
    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Door" && color == other.GetComponentInParent<NewDoor>().color)
        {
            // Debug.Log("Enter");
            OpenDoors();
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Door" && color == other.GetComponentInParent<NewDoor>().color)
        {
            // Debug.Log("Exit");
            CloseDoors();
        }
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
