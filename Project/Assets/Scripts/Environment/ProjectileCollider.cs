using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileCollider : MonoBehaviour {

    // Use this for initialization
    void Start() {

    }

    // Update is called once per frame
    void Update() {

    }

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log(other.gameObject.name + " - " + LayerMask.LayerToName(other.gameObject.layer));
        if (other.gameObject.layer == LayerMask.NameToLayer("ClosedDoors") || other.gameObject.layer == LayerMask.NameToLayer("Walls"))
        {
            Destroy(gameObject);
        }

        else if (other.gameObject.layer == LayerMask.NameToLayer("OpenDoors"))
        {
            gameObject.GetComponent<ProjectileController>().projectileManager.UpdateProjectilePositionIfNeeded(gameObject, other.gameObject);
        }

        else if (other.gameObject.layer == LayerMask.NameToLayer("Bomb"))
        {
            gameObject.GetComponent<ProjectileController>().projectileManager.DestroyFloatingStone();
            Destroy(gameObject);
        }
    }



}
