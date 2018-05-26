using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaserLevelTargets : MonoBehaviour {

    private LaserLevelManager manager;
    private bool wasHit = false;

    public Material notHitMaterial;
    public Material hitMaterial;

    public bool WasHit { get { return wasHit; } }

    // Use this for initialization
    void Start () {
        manager = FindObjectOfType<LaserLevelManager>();
	}

    public void OnHitTarget()
    {
        if (!wasHit)
        {
            wasHit = true;
            manager.markAsHit();
            GetComponent<Renderer>().material = hitMaterial;
        }
    }
}
