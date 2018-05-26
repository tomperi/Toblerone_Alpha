using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaserTrigger : MonoBehaviour {

    public Laser laser;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag.Equals("Player"))
        {
           Debug.Log("Triggered");
            laser.ShootLaser();
        }
    }

    private void OnTriggerExit(Collider other)
    {
        laser.resetLaser();  
    }
}
