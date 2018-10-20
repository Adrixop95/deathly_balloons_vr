using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(RoadCreator2D))]
public class RoadEditor2D : Editor {

    RoadCreator2D creator;

    void OnSceneGUI() {
        if ( creator.automatycznaAktualizacja && Event.current.type == EventType.Repaint ) {
            creator.UpdateRoad();
        }
    }

    void OnEnable() {
        creator = (RoadCreator2D)target;
    }
	
}
