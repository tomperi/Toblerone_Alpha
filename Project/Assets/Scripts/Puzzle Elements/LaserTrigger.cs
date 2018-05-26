using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaserTrigger : MonoBehaviour {

    private Laser laser;
    private GameController gameController;

    private void Start()
    {
        laser = FindObjectOfType<Laser>();
        gameController = FindObjectOfType<GameController>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag.Equals("Player"))
        {
            gameController.zoomInOut();
            laser.ShootLaser();
        }
    }

    private void OnTriggerExit(Collider other)
    {
        laser.resetLaser();  
    }
}
