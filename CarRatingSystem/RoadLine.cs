using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine.UI;

public class RoadLine : MonoBehaviour {

    public Transform[] roadPoints;
    public CarHandler[] cars;
    public GameObject colliderPrefab;

    public Text[] lines;

	void Start () 
    {
        //No worries - runs only once :)
        for (int i = 1; i < roadPoints.Length - 1; ++i)
        {
            GameObject collider = Instantiate(colliderPrefab) as GameObject;
            collider.transform.position = roadPoints[i].transform.position;

            Vector3 nextChunkDir = roadPoints[i + 1].transform.position - roadPoints[i].transform.position;
            Vector3 lastChunkDir = roadPoints[i - 1].transform.position - roadPoints[i].transform.position;

            collider.transform.right = nextChunkDir;
            float angle = Vector3.Angle(nextChunkDir, lastChunkDir);

            Vector3 rot = collider.transform.rotation.eulerAngles;
            rot.y -= angle / 2;

            collider.transform.rotation  = Quaternion.Euler(rot);

            RoadWall wallScript = collider.GetComponent<RoadWall>();
            wallScript.setWallIndex(i);

            Debug.DrawLine(collider.transform.position, collider.transform.position + collider.transform.forward.normalized * 50, Color.blue);
        }
	}
	
	void Update () 
    {
        //array is sorting depending on car positon
        Array.Sort<CarHandler>(cars, CompareCarPos);

        lines[2].text = "First: "   + cars[0].gameObject.name;
        lines[1].text = "Second: "  + cars[1].gameObject.name;
        lines[0].text = "Third: "   + cars[2].gameObject.name;
	}

    private int CompareCarPos(CarHandler a, CarHandler b) 
    {
        if (a.getLastPtIndex() > b.getLastPtIndex())
        {
            return 1;
        }

        if (a.getLastPtIndex() < b.getLastPtIndex())
        {
            return -1;
        }

        if (a.getLastPtIndex() == b.getLastPtIndex())
        {
            Vector3 roadDir = a.getRoadDirection();
            Vector3 abDir = a.gameObject.transform.position - b.gameObject.transform.position;

            float dot = Vector3.Dot(roadDir, abDir);

            if (dot > 0)
            {
                return 1;
            }

            if (dot < 0)
            {
                return -1;
            }

            if (Mathf.Approximately(dot, 0.0f)) 
            {
                return 0;
            }
        }

        return 1;
    }

    private float explosionRadius = 0.5F;

    void OnDrawGizmos()
    {
        if (roadPoints.Length > 1)
        {
            for (int i = 0; i < roadPoints.Length; ++i)
            {
                Gizmos.color = Color.white;
                Gizmos.DrawWireSphere(roadPoints[i].position, explosionRadius);

                char lastChar = roadPoints[i].name[roadPoints[i].name.Length - 1];
                if (!(lastChar >= '0' && lastChar <= '9'))
                {
                    roadPoints[i].name += (i + 1);
                }

                if (i > 0)
                {
                    Gizmos.color = Color.red;
                    Gizmos.DrawLine(roadPoints[i - 1].position, roadPoints[i].position);
                }

            }
        }
    }

    public void setCars(CarHandler[] carArray)
    {
        cars = carArray;
    }
}
