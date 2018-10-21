using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(RoadCreator3D))]
public class RoadEditor3D : Editor {

    RoadCreator3D creator;

    void OnSceneGUI() {
        if ( creator.automatycznaAktualizacja && Event.current.type == EventType.Repaint ) {
            creator.UpdateRoad();
        }
    }

    void OnEnable() {
        creator = (RoadCreator3D)target;
    }
	
}
