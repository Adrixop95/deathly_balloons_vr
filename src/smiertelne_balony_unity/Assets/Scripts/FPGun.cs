using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FPGun : MonoBehaviour {

    // https://www.youtube.com/watch?v=Ka_cOVSTx6c

    public float timeBetweenBullets = 0.15f;
    public float range = 10f;

    float timer;

    LineRenderer gunLine;
    float effectsDisplayTime = 0.2f;

    bool isShooting = false;

    Camera Camera;

    Transform gunEnd;

    private void Awake() {
        gunEnd = GameObject.FindWithTag("GunEnd").transform;
        gunLine = gunEnd.GetComponent<LineRenderer>();
        Camera = GetComponent<Camera>();
    }

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        timer += Time.deltaTime;

        if (isShooting == true  && timer >= effectsDisplayTime) {
            Shoot();
        }

        if (timer >= timeBetweenBullets * effectsDisplayTime) {
            DisableEffects();
        }

        if (Input.GetButtonDown("Fire1")) {
            isShooting = true;
        } else {
            isShooting = false;
        }		
	}

    public void DisableEffects() {
        gunLine.enabled = false;
    }

    void Shoot() {
        gunEnd = GameObject.FindWithTag("GunEnd").transform;
        gunLine = gunEnd.GetComponent<LineRenderer>();

        timer = 0f;

        gunLine.enabled = true;

        Vector3 rayOrigin = Camera.ViewportToWorldPoint(new Vector3(0.5f, 0.5f, 0.5f));
        RaycastHit hit;

        gunLine.SetPosition(0, gunEnd.position);
        gunLine.SetPosition(1, gunEnd.position + (Camera.transform.forward * range));
    }
}
