using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StoneTrigger : MonoBehaviour {

    private ProjectileManager projectileManager;

    private void Start()
    {
        projectileManager = FindObjectOfType<ProjectileManager>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag.Equals("Player"))
        {
            projectileManager.SpawnProjectile();
        }
    }

}
