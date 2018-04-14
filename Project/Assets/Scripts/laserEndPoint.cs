using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class laserEndPoint : MonoBehaviour {

    private Laser laser;
    private bool wasHit;

    public Material notHitMaterial;
    public Material hitMaterial;

	// Use this for initialization
	void Start () {
        wasHit = false;
        laser = FindObjectOfType<Laser>();
        GetComponent<Renderer>().material = notHitMaterial;
	}
	
	public void OnHitTarget()
    {
        if (!wasHit)
        {
            wasHit = true;
            laser.isTargetHit = true;
            GetComponent<Renderer>().material = hitMaterial;
        }
        
    }
}
