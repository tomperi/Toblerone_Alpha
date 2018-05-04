using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mirror : MonoBehaviour
{
    private Laser laser;
    public LayerMask layerMask;

    // Use this for initialization
    void Start()
    {
        laser = FindObjectOfType<Laser>();
    }

    public List<Vector3> OnMirrorLaserHit(GameObject origin)
    {
        List<Vector3> pointsList = new List<Vector3>();

        RaycastHit hit;

        if (Physics.Raycast(transform.position, transform.TransformDirection(Vector3.forward), out hit, 10000f, laser.allButOpenDoorLayerMask))
        {
            Vector3 point = hit.transform.GetComponent<Renderer>().bounds.center;

            if (hit.transform.gameObject != origin)
            {
                if (hit.transform.gameObject.layer != layerMask)
                {
                    pointsList.Add(point);
                }

                if (hit.transform.GetComponent<Mirror>() != null)
                {
                    List<Vector3> mirrorPointsList = hit.transform.GetComponent<Mirror>().OnMirrorLaserHit(this.gameObject);

                    foreach (Vector3 mirrorPoint in mirrorPointsList)
                    {
                        pointsList.Add(mirrorPoint);
                    }
                }

                if (hit.transform.GetComponent<laserEndPoint>() != null)
                {
                    hit.transform.GetComponent<laserEndPoint>().OnHitTarget();
                }
            }
        }

        return pointsList;
    }
}
