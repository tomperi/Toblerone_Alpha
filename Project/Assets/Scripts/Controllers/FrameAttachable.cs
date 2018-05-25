using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FrameAttachable : MonoBehaviour {

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            other.transform.SetParent(this.transform.parent);
            Debug.Log("Grandma moved to " + transform.parent.gameObject.name);
        }
    }
}
