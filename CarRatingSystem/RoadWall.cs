using UnityEngine;
using System.Collections;

public class RoadWall : MonoBehaviour {

    private int wallIndex = 0;

	void Start () 
    {
	    
	}

    void OnTriggerExit(Collider other)
    {
        CarHandler carHandler = other.gameObject.GetComponent<CarHandler>();

        if (carHandler != null)
        {
            Vector3 carVector = other.gameObject.transform.position - transform.position;

            if (Vector3.Dot(transform.forward, carVector) < 0)
            {
                carHandler.setLastPtIndex(wallIndex);
            }
            else
            {
                carHandler.setLastPtIndex(wallIndex - 1);
            }
        }
    }



    public void setWallIndex(int index)
    {
        wallIndex = index;
    }

    public void OnDrawGizmos() 
    {
        Gizmos.color = Color.blue;
        //forward vector for debug purpose
        Gizmos.DrawLine(collider.transform.position, collider.transform.position + collider.transform.forward.normalized * 10);
    }



}
