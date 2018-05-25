using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FrameAttachable : MonoBehaviour {

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player" || other.tag == "Projectile")
        {
            PlaceObjectUnderParent(other.gameObject);
            Debug.Log(other.tag + " moved to " + transform.parent.gameObject.name);

        }
    }
    
    private void PlaceObjectUnderParent(GameObject grandma)
    {
        grandma.transform.SetParent(this.transform.parent);
    }
}
