using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraRaycast : MonoBehaviour {

    public  float       raycast_max_delay = 60f;
    public  float       raycast_distance = 10f;
    public  LayerMask[] ignore_layer;
    private GameObject  last_object;
    private float       raycast_timer;
	
	/// <summary> Funkcja Update </summary>
	void Update () {
		RaycastHit();
	}

    /// <summary> Wybór obiektu przez spoglądanie na niego przez określoną ilość czasu </summary>
    private void RaycastHit() {
        Ray ray = new Ray( transform.position, transform.forward );
        RaycastHit hit;

        if ( Physics.Raycast( ray, out hit, raycast_distance ) ) {
            GameObject hitObject = hit.collider.gameObject;
            if ( hitObject != last_object ) {
                raycast_timer = 0f;
                last_object = hitObject;
            }
            raycast_timer += Time.deltaTime;
            if ( raycast_timer > raycast_max_delay ) { raycast_timer = raycast_max_delay; }
        } else {
            raycast_timer = 0f;
            last_object = null;
        }
    }

    public GameObject GetSelectedObject() { return last_object; }
    public float GetSelectedActive() { return raycast_timer; }
}
