using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PathPlacer3D : MonoBehaviour {

    public float spacing = 0.25f;
    public float resolution = 1;
    public float size = 0.25f;
	
	void Start () {
        Vector3[] points = FindObjectOfType<PathCreator3D>().path.CalculateEvenlySpacedPoints( spacing, resolution );
        int gIndex = 0;
        foreach ( Vector3 p in points ) {
            GameObject g = GameObject.CreatePrimitive( PrimitiveType.Sphere );
            g.transform.parent = transform;
            g.transform.position = p;
            g.transform.localScale = Vector3.one * size;
            g.name = "Point " + ((gIndex>=100) ? gIndex.ToString() : (gIndex>=10) ? "0"+gIndex.ToString() : "00"+gIndex.ToString() );
            g.GetComponent<SphereCollider>().enabled = false;
            gIndex++;
        }
	}
	
	
}
