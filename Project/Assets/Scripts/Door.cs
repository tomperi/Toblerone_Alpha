using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Door : MonoBehaviour {

    Animator animator;
    bool doorOpen;

	void Start () {
        doorOpen = false;
        animator = GetComponent<Animator>();
	}
	
	void Update () {
		
	}

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Door")
        {
            doorOpen = true;
            Doors("Open");
            //Debug.Log("Open");
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if ((doorOpen) && (other.gameObject.tag == "Door")) 
        {
            doorOpen = false;
            Doors("Close");
            //Debug.Log("Close");
        }
    }

    void Doors(string direction)
    {
        animator.SetTrigger(direction);
    }
}
