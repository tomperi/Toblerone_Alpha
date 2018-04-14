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

    public void OnMirrorLaserHit()
    {
        StartCoroutine(waitBeforeCastingRay());
    }

    IEnumerator waitBeforeAddingPoint(Vector3 point)
    {
        yield return new WaitForSeconds(0.1f);
        laser.addPoint(point, false);
    }

    IEnumerator waitBeforeCastingRay()
    {
        yield return new WaitForSeconds(0.2f);

        RaycastHit hit;

        //transform.Rotate(new Vector3(0f, 0f, 0f));

        if (Physics.Raycast(transform.position, transform.TransformDirection(Vector3.forward), out hit, 10000f))
        {
            //Debug.Log("Mirror hit something");
            Vector3 point = hit.transform.GetComponent<Renderer>().bounds.center;
            //Vector3 point = hit.transform.position;

            if (hit.transform.GetComponent<Mirror>() != null)
            {
                hit.transform.GetComponent<Mirror>().OnMirrorLaserHit();
            }

            if (hit.transform.GetComponent<laserEndPoint>() != null)
            {
                hit.transform.GetComponent<laserEndPoint>().OnHitTarget();
            }

            if (hit.transform.gameObject.layer != layerMask)
            {
                StartCoroutine(waitBeforeAddingPoint(point));
            }
            else
            {
                //Debug.Log("got nothing");
            }

            //hit.transform.gameObject.SetActive(false);

        }

        //transform.Rotate(new Vector3(0f, 0, 0f));
    }
}
