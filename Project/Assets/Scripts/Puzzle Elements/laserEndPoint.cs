using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class laserEndPoint : MonoBehaviour {

    private Laser laser;
    private bool wasHit;

    public Material notHitMaterial;
    public Material hitMaterial;

    private LevelExit exit;

    public bool WasHit {get {return wasHit;}}

    // Use this for initialization
    void Start () {
        wasHit = false;
        laser = FindObjectOfType<Laser>();
        GetComponent<Renderer>().material = notHitMaterial;

        exit = FindObjectOfType<LevelExit>();
        exit.gameObject.SetActive(false);
	}
	
	public void OnHitTarget()
    {
        if (!wasHit)
        {
            wasHit = true;
            laser.levelHitTarget();
            GetComponent<Renderer>().material = hitMaterial;
            StartCoroutine(setExitActive());
        }
    }

    IEnumerator setExitActive()
    {
        yield return new WaitForSeconds(1f);
        exit.gameObject.SetActive(true);
    }
}
