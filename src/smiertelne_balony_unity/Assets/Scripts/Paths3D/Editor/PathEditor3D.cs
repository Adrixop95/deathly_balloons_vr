using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(PathCreator3D))]
public class PathEditor3D : Editor {

    PathCreator3D creator;
    Path3D Path {
        get { return creator.path; }
    }

    const float segmentSelectDistanceThreshold = 0.1f;
	const float minDstToAnchorThreshold = 0.5f;
    int selectedSegmentIndex = -1;

    public override void OnInspectorGUI() {
        base.OnInspectorGUI();

        EditorGUI.BeginChangeCheck();
		if ( GUILayout.Button("Nowa ścieżka") ) {
            Undo.RecordObject( creator, "Nowa ścieżka" );
            creator.CreatePath();
		}

        GUILayout.Label( "Dodawanie punktu [SHIFT + LPM]" );
        GUILayout.Label( "Usuwanie punktu [CTRL + LPM]" );

        bool isClosed = GUILayout.Toggle( Path.IsClosed, "Zamknięta ścieżka" );
        if ( isClosed != Path.IsClosed ) {
            Undo.RecordObject( creator, "Przełącz zamknięcie ścieżki" );
            Path.IsClosed = isClosed;
        }

        bool autoSetControlPoints = GUILayout.Toggle( Path.AutoSetControlPoints, "Automatyczne ustawianie punktów kontrolych" );
        if ( autoSetControlPoints != Path.AutoSetControlPoints ) {
            Undo.RecordObject(creator, "Przełącz Automatyczne ustawianie punktów kontrolych");
            Path.AutoSetControlPoints = autoSetControlPoints;
        }

        bool lockYAxis = GUILayout.Toggle( creator.LockYAxis, "Zablokuj oś Y" );
        if ( lockYAxis != creator.LockYAxis ) {
            Undo.RecordObject(creator, "Przełącz blokadę osi Y");
            creator.lock_yAxis = lockYAxis;
        }

        if (EditorGUI.EndChangeCheck()) {
            SceneView.RepaintAll();
        }
    }

    void OnSceneGUI() {
        Input();
        Draw();
    }

    void Input() {
        Event guiEvent = Event.current;
        Vector3 mousePos = HandleUtility.GUIPointToWorldRay( guiEvent.mousePosition ).origin;
        if ( creator.LockYAxis ) { mousePos.y = 0; }

        if ( guiEvent.type == EventType.MouseDown && guiEvent.button == 0 && guiEvent.shift ) {
            if (selectedSegmentIndex != -1) {
                Undo.RecordObject( creator, "Wstaw segment" );
                Path.SplitSegment( mousePos, selectedSegmentIndex );
				
            } else if ( !Path.IsClosed ) {
                Undo.RecordObject( creator, "Dodaj segment" );
                Path.AddSegment( mousePos );
            }
        }

        if ( guiEvent.type == EventType.MouseDown && guiEvent.button == 0 && guiEvent.control ) {
            float minDstToAnchor = creator.rozmiarPunktu * minDstToAnchorThreshold;
            int closestAnchorIndex = -1;

            for ( int i = 0; i < Path.NumPoints; i += 3 ) {
                float dst = Vector3.Distance( mousePos, Path[i] );
                if ( dst < minDstToAnchor ) {
                    minDstToAnchor = dst;
                    closestAnchorIndex = i;
                }
            }

            if ( closestAnchorIndex != -1 ) {
                Undo.RecordObject( creator, "Usuń segment" );
                Path.DeleteSegment( closestAnchorIndex );
            }
        }

        if (guiEvent.type == EventType.MouseMove) {
            float minDstToSegment = segmentSelectDistanceThreshold;
            int newSelectedSegmentIndex = -1;

            for ( int i = 0; i < Path.NumSegments; i++ ) {
                Vector3[] points = Path.GetPointsInSegment(i);
                float dst = HandleUtility.DistancePointBezier( mousePos, points[0], points[3], points[1], points[2] );
				
                if ( dst < minDstToSegment ) {
                    minDstToSegment = dst;
                    newSelectedSegmentIndex = i;
                }
            }

            if ( newSelectedSegmentIndex != selectedSegmentIndex ) {
                selectedSegmentIndex = newSelectedSegmentIndex;
                HandleUtility.Repaint();
            }
        }

        HandleUtility.AddDefaultControl(0);
    }

    void Draw() {
        for ( int i = 0; i < Path.NumSegments; i++ ) {
            Vector3[] points = Path.GetPointsInSegment(i);
            if ( creator.punktyKontrolne ) {
                Handles.color = Color.black;
                Handles.DrawLine(points[1], points[0]);
                Handles.DrawLine(points[2], points[3]);
            }
            Color segmentCol = ((i == selectedSegmentIndex && Event.current.shift) ? creator.kolorWybranegoSegmentu : creator.kolorSegmentu);
            Handles.DrawBezier( points[0], points[3], points[1], points[2], segmentCol, null, 2 );
        }

        for ( int i = 0; i < Path.NumPoints; i++ ) {
            if ( i%3 == 0 || creator.punktyKontrolne ) {
                Handles.color = ((i%3 == 0) ? creator.kolorPunktu : creator.kolorPunktuKontrolnego);
                float handleSize = ((i%3 == 0) ? creator.rozmiarPunktu : creator.rozmiarPunktuKontrolnego);
                Vector3 newPos = Handles.FreeMoveHandle( Path[i], Quaternion.identity, handleSize, Vector3.zero, Handles.CylinderHandleCap );
                
                if ( creator.LockYAxis ) { newPos.y = 0; }
                if ( Path[i] != newPos ) {
                    Undo.RecordObject( creator, "Przesuń punkt" );
                    Path.MovePoint( i, newPos );
                }
            }
        }
    }

    void OnEnable() {
        creator = (PathCreator3D)target;
        if ( creator.path == null ) {
            creator.CreatePath();
        }
    }
	
}
