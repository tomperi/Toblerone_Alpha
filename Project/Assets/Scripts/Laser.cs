using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Laser : MonoBehaviour
{
    public LayerMask allButOpenDoorLayerMask;
    public bool isTargetHit;

    public float temp;

    private LineRenderer lineRenderer;
    private Vector3[] linePositions;
    

    // Use this for initialization
    void Start()
    {
        lineRenderer = GetComponent<LineRenderer>();
        linePositions = new Vector3[1];
        linePositions[0] = transform.position;
        lineRenderer.positionCount = 1;
        lineRenderer.SetPositions(linePositions);
        isTargetHit = false;

        
    }

    public void ShootLaser()
    {
        resetLaser();
        if (!isTargetHit)
        {
            StartCoroutine(shootLaser());
        }
    }

    private void resetLaser()
    {
        updateOriginPosition();
        Vector3[] newLinePositions = new Vector3[1];

        newLinePositions[0] = linePositions[0];

        linePositions = newLinePositions;
        lineRenderer.positionCount = linePositions.Length;
        lineRenderer.SetPositions(linePositions);
    }

    IEnumerator shootLaser()
    {
        yield return new WaitForSeconds(1f);

        RaycastHit hit;
        
        if (Physics.Raycast(transform.position, transform.TransformDirection(Vector3.forward), out hit, 10000f, allButOpenDoorLayerMask))
        {
            Vector3 point = hit.transform.GetComponent<Renderer>().bounds.center;

            if (hit.transform.GetComponent<Mirror>() != null)
            {
                hit.transform.GetComponent<Mirror>().OnMirrorLaserHit();
            }

            addPoint(point, true);
        }
        else
        {
            resetLaser();
        }

    }

    private void updateOriginPosition()
    {
        linePositions[0] = transform.position;
    }

    public void addPoint(Vector3 newPoint, bool local)
    {
       // Debug.Log("add point triggered " + newPoint.x + "," + newPoint.y + "," + newPoint.z + " " + local);
        Vector3[] newLinePositions = new Vector3[linePositions.Length + 1];

        for (int i = 0; i < linePositions.Length; i++)
        {
            newLinePositions[i] = linePositions[i];
        }

        newLinePositions[linePositions.Length] = newPoint;
        //Debug.Log("Before " + linePositions.Length + " " + lineRenderer.positionCount);
        linePositions = newLinePositions;
        lineRenderer.positionCount = linePositions.Length;
        //Debug.Log("After " + linePositions.Length + " " + lineRenderer.positionCount);
        lineRenderer.SetPositions(linePositions);
    }
}
