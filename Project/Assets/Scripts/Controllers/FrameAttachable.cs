using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FrameAttachable : MonoBehaviour {

    private FrameManager frameManager;
    private Frame frameController;

    private void Start()
    {
        frameManager = FindObjectOfType<FrameManager>();
        frameController = GetComponent<Frame>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            PlaceObjectUnderParent(other.gameObject);
            Debug.Log("Grandma moved to " + transform.parent.gameObject.name);

        }
    }
    private void PlaceObjectUnderParent(GameObject grandma)
    {
        grandma.transform.SetParent(this.transform.parent);
    }
}
