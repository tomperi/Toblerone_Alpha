using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class laserEndPoint : MonoBehaviour {

    private Laser laser;
    private bool wasHit;

    public Material notHitMaterial;
    public Material hitMaterial;

    private MoveToNextLevelScript moveToNextLevelScript;

    public bool WasHit {get {return wasHit;}}

    // Use this for initialization
    void Start () {
        wasHit = false;
        laser = FindObjectOfType<Laser>();
        GetComponent<Renderer>().material = notHitMaterial;

        moveToNextLevelScript = FindObjectOfType<MoveToNextLevelScript>();
	}
	
	public void OnHitTarget()
    {
        if (!wasHit)
        {
            wasHit = true;
            laser.levelHitTarget();
            GetComponent<Renderer>().material = hitMaterial;
        }
    }
}
