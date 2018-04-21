using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileCollider : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    private void OnCollisionEnter(Collision other)
    {
        Debug.Log("hi");
        Debug.Log(other.gameObject.name);
        if (other.gameObject.layer == LayerMask.NameToLayer("ClosedDoors"))
        {
            Destroy(this.gameObject);
        }
    }

    private void OnCollisionStay(Collision other)
    {
        Debug.Log("hi");
        Debug.Log(other.gameObject.name);
        if (other.gameObject.layer == LayerMask.NameToLayer("ClosedDoors"))
        {
            Destroy(this.gameObject);
        }
    }

    private void OnCollisionExit(Collision other)
    {
        Debug.Log("hi");
        Debug.Log(other.gameObject.name);
        if (other.gameObject.layer == LayerMask.NameToLayer("ClosedDoors"))
        {
            Destroy(this.gameObject);
        }
    }

}
