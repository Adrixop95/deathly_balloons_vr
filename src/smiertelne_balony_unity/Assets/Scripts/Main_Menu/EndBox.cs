using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EndBox : MonoBehaviour {

    public string next_scene_name;

    void OnTriggerEnter(Collider collider) {
        if ( collider != null ) {
            if ( collider.name == "CameraRig" || collider.name == "Camera") {
                SceneManager.LoadScene( next_scene_name, LoadSceneMode.Single );
            }
        }
    }

}
