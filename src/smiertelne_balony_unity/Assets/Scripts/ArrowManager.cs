using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR;

public class ArrowManager : MonoBehaviour {

    private GameObject currentArrow;
    private bool isAttached = false;

    public GameObject arrowPrefab;
    public SteamVR_TrackedObject trackedObj;
    public GameObject stringAttachPoint;
    public GameObject arrowStartPoint;
    public static ArrowManager Instance;
    public GameObject stringStartPoint;

    void Awake() {
        if (Instance == null) {
            Instance = this;
        }
    }

    void OnDestroy() {
        if (Instance == this) {
            Instance = null;
        }
    }

    // Use this for initialization
    void Start() {
		
	}
	
	// Update is called once per frame
	void Update() {
        attachArrow();
        pullString();
	}

    private void pullString() {
        if (isAttached)
        {
            float dist = (stringStartPoint.transform.position - trackedObj.transform.position).magnitude;
            stringAttachPoint.transform.localPosition = stringStartPoint.transform.localPosition + new Vector3(5f * dist, 0f, 0f);


            var device = SteamVR_Controller.Input((int)trackedObj.index);
            if (device.GetTouchUp(SteamVR_Controller.ButtonMask.Trigger)) {
                fire();
            }
        }
    }

    public void fire() {
        float dist = (stringStartPoint.transform.position - trackedObj.transform.position).magnitude;

        currentArrow.transform.parent = null;
        currentArrow.GetComponent<Arrow>().fired();

        Rigidbody r = currentArrow.GetComponent<Rigidbody>();
        r.velocity = currentArrow.transform.forward * 10f * dist;
        r.useGravity = true;

        stringAttachPoint.transform.position = stringStartPoint.transform.position;

        currentArrow = null;
        isAttached = false;
    }

    public void attachArrow() {
        if (currentArrow == null) {
            currentArrow = Instantiate(arrowPrefab);
            currentArrow.transform.parent = trackedObj.transform;
            currentArrow.transform.localPosition = new Vector3(0f, 0f, .354f);
            currentArrow.transform.localRotation = Quaternion.identity;
        }
    }

    public void attachBowToArrow() {
        currentArrow.transform.parent = stringAttachPoint.transform;
        currentArrow.transform.localPosition = arrowStartPoint.transform.localPosition;
        currentArrow.transform.rotation = arrowStartPoint.transform.rotation;

        isAttached = true;
    }
}
