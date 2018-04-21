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

    private void OnTriggerEnter(Collider other)
    {
        //Debug.Log(other.gameObject.name + " - " + LayerMask.LayerToName(other.gameObject.layer));
        if (other.gameObject.layer == LayerMask.NameToLayer("ClosedDoors") || other.gameObject.layer == LayerMask.NameToLayer("Walls"))
        {
            Destroy(this.gameObject);
        }

        else if (other.gameObject.layer == LayerMask.NameToLayer("OpenDoors")) 
        {
            this.gameObject.GetComponent<ProjectileController>().projectileManager.UpdateProjectilePositionIfNeeded(this.gameObject,other.gameObject);
        }
    }

    

}
