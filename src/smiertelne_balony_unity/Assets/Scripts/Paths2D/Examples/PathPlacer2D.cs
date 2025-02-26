﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PathPlacer2D : MonoBehaviour {

    public float spacing = .1f;
    public float resolution = 1;
	
	void Start () {
        Vector2[] points = FindObjectOfType<PathCreator2D>().path.CalculateEvenlySpacedPoints( spacing, resolution );
        foreach ( Vector2 p in points ) {
            GameObject g = GameObject.CreatePrimitive( PrimitiveType.Sphere );
            g.transform.position = p;
            g.transform.localScale = Vector3.one * spacing * 0.5f;
        }
	}
	
	
}
