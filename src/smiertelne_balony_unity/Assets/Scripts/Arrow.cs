using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR;

public class Arrow : MonoBehaviour {

    private bool isAttached = false;
    private bool isFired = false;

    void onTriggerStay() {
        attachArrow();
    }

	void onTriggerEnter() {
        attachArrow();
    }

    void Update() {
        if (isFired) {
            transform.LookAt(transform.position + transform.GetComponent<Rigidbody>().velocity);
        }
    }

    public void fired(){
        isFired = true;
    }

    private void attachArrow() {
        var device = SteamVR_Controller.Input((int)ArrowManager.Instance.trackedObj.index);
        if (!isAttached && device.GetTouchDown(SteamVR_Controller.ButtonMask.Trigger)) {
            ArrowManager.Instance.attachBowToArrow();
            isAttached = true;
        }
    }
}
