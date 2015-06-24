using UnityEngine;
using System.Collections;

[RequireComponent(typeof(LineRenderer))]
public class CarHandler : MonoBehaviour {

    public bool drawDebugDir = true;

    private RoadLine        roadLine;
    private int             lastPointIndex = 0;
    private LineRenderer    lineRenderer;

	void Start () 
    {
        roadLine        = GameObject.FindObjectOfType<RoadLine>();

        if (drawDebugDir)
        {
            lineRenderer = GetComponent<LineRenderer>();
            lineRenderer.SetColors(Color.green, Color.green);
        }
	}
	
	void Update () 
    {
        if (drawDebugDir)
        {
            Vector3 prevPoint = roadLine.roadPoints[lastPointIndex].position;
            Vector3 nextPoint = roadLine.roadPoints[lastPointIndex + 1].position;
            Vector3 carPos = transform.position;

            Vector3 roadChunkDirection = nextPoint - prevPoint;

            lineRenderer.SetPosition(0, carPos);
            lineRenderer.SetPosition(1, carPos + roadChunkDirection.normalized * 5.0f);
        }
    }

    void OnDrawGizmos()
    {
        if (roadLine != null)
        {
            Transform[] roadPoints = roadLine.roadPoints;
            if (lastPointIndex + 1 < roadPoints.Length)
            {
                Gizmos.color = Color.blue;
                Gizmos.DrawLine(transform.position, roadPoints[lastPointIndex].position);
                Gizmos.DrawLine(transform.position, roadPoints[lastPointIndex + 1].position);
            }
        }
        
    }

    public Vector3 getRoadDirection()
    {
        Vector3 prevPoint = roadLine.roadPoints[lastPointIndex].position;
        Vector3 nextPoint = roadLine.roadPoints[lastPointIndex + 1].position;

        return nextPoint - prevPoint;
    }

    public void incPtIndex()
    {
        ++lastPointIndex;
    }


    public void decPtIndex()
    {
        --lastPointIndex;
    }

    public int getLastPtIndex()
    {
        return lastPointIndex;
    }

    public void setLastPtIndex(int index)
    {
        lastPointIndex = index;
    }
}
