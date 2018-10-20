using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PathCreator2D : MonoBehaviour {

    [HideInInspector] public Path2D path;

	public Color kolorPunktu = Color.red;
    public Color kolorPunktuKontrolnego = Color.white;
    public Color kolorSegmentu = Color.green;
    public Color kolorWybranegoSegmentu = Color.yellow;
    public float rozmiarPunktu = 0.1f;
    public float rozmiarPunktuKontrolnego = 0.1f;
    public bool punktyKontrolne = true;

    public void CreatePath() {
        path = new Path2D( transform.position );
    }

    void Reset() {
        CreatePath();
    }
	
}
