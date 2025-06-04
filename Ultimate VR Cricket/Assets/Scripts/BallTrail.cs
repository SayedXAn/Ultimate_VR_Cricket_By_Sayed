using UnityEngine;
using System.Collections.Generic;

[RequireComponent(typeof(LineRenderer))]
public class BallTrail : MonoBehaviour
{
    //public float pointSpacing = 0.1f; // Distance between points
    //public float maxTrailLength = 5f; // Optional: remove older points over time

    ////private LineRenderer lineRenderer;
    //private LineRenderer lineRenderer;
    //private List<Vector3> trailPoints = new List<Vector3>();
    //private Vector3 lastPoint;

    //void Start()
    //{
    //    lineRenderer = GetComponent<LineRenderer>();
    //    lineRenderer.positionCount = 0;
    //    lastPoint = transform.position;
    //}

    //void Update()
    //{
    //    if (!GetComponent<Ball>().hitByBat) return;

    //    float distance = Vector3.Distance(transform.position, lastPoint);
    //    if (distance >= pointSpacing)
    //    {
    //        trailPoints.Add(transform.position);
    //        lineRenderer.positionCount = trailPoints.Count;
    //        lineRenderer.SetPositions(trailPoints.ToArray());
    //        lastPoint = transform.position;

    //        // Optional: limit trail length
    //        float totalLength = 0f;
    //        for (int i = trailPoints.Count - 2; i >= 0; i--)
    //        {
    //            totalLength += Vector3.Distance(trailPoints[i], trailPoints[i + 1]);
    //            if (totalLength > maxTrailLength)
    //            {
    //                trailPoints.RemoveAt(0);
    //                lineRenderer.positionCount = trailPoints.Count;
    //            }
    //        }
    //    }
    //}

    //private void OnCollisionEnter(Collision collision)
    //{
    //    if (collision.gameObject.CompareTag("bat"))
    //    {
    //        GetComponent<Ball>().hitByBat = true;
    //        trailPoints.Clear();
    //        lineRenderer.positionCount = 0;
    //        lastPoint = transform.position;
    //    }
    //}
}
