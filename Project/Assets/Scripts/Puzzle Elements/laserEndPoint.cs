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
            StartCoroutine(markLaserHit());
            //laser.isTargetHit = true;
          //  laser.resetLaser();
            GetComponent<Renderer>().material = hitMaterial;
        }
        
    }

    IEnumerator markLaserHit()
    {
        yield return new WaitForSeconds(0.5f);
        laser.levelHitTarget();
        yield return new WaitForSeconds(2f);
        moveToNextLevelScript.OnLevelComplete();
    }
}
