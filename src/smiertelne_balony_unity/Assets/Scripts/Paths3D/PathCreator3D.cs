using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PathCreator3D : MonoBehaviour {

    [HideInInspector] public Path3D path;
    [HideInInspector] public bool lock_yAxis;

	public Color kolorPunktu = Color.red;
    public Color kolorPunktuKontrolnego = Color.white;
    public Color kolorSegmentu = Color.green;
    public Color kolorWybranegoSegmentu = Color.yellow;
    public float rozmiarPunktu = 0.1f;
    public float rozmiarPunktuKontrolnego = 0.1f;
    public bool punktyKontrolne = true;

    public void CreatePath() {
        path = new Path3D( transform.position );
    }

    public bool LockYAxis {
        get { return lock_yAxis; }
        set {
            if ( lock_yAxis != value ) {
                lock_yAxis = value;
            }
        }
    }

    void Reset() {
        CreatePath();
    }
	
}
