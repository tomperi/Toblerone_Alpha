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

    public void levelHitTarget()
    {
        isTargetHit = true;
        lineRenderer.positionCount = 0;

    }

    public void resetLaser()
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
        List<Vector3> mirrorPointsList = new List<Vector3>();
        if (Physics.Raycast(transform.position, transform.TransformDirection(Vector3.forward), out hit, 10000f, allButOpenDoorLayerMask))
        {
            Vector3 point = hit.transform.GetComponent<Renderer>().bounds.center;
            if (hit.transform.GetComponent<Mirror>() != null)
            {
                mirrorPointsList = hit.transform.GetComponent<Mirror>().OnMirrorLaserHit(this.gameObject);
            }

            addPoint(point, true);
            Vector3[] mirrorPointsArray = mirrorPointsList.ToArray();
            for (int i = 0; i < mirrorPointsArray.Length; i++)
            {
                addPoint(mirrorPointsArray[i], true);
            }
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
        Vector3[] newLinePositions = new Vector3[linePositions.Length + 1];

        for (int i = 0; i < linePositions.Length; i++)
        {
            newLinePositions[i] = linePositions[i];
        }

        newLinePositions[linePositions.Length] = newPoint;
        linePositions = newLinePositions;
        lineRenderer.positionCount = linePositions.Length;
        lineRenderer.SetPositions(linePositions);
    }
}
