using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColliderBallon : MonoBehaviour {

    void OnTriggerExit(Collider other) {
        if ( other.gameObject != null ) {
            if ( other.gameObject.name == "MotherBalloon" ) {
                Debug.Log(name.ToString() + " >> " + other.name.ToString());
                GetComponent<CapsuleCollider>().isTrigger = false;
            }
        }
    }

}
