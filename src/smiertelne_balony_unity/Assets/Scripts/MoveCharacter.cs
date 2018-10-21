using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveCharacter : MonoBehaviour {

    public PathCreator3D pathObject;
    public float speed = 1f;
    public float rotation = 1f;
    public float spacing = 0.1f;
    public float resolution = 1;
    public bool infinite_loop = true;

    private int current_position;
    private Vector3[] points;

	/// <summary> Funkcja Inicjalizacji </summary>
	void Start () {
		points  =   pathObject.path.CalculateEvenlySpacedPoints( spacing, resolution );
	}
	
	/// <summary> Funkcja Update </summary>
	void Update () {
        if ( GamePause.IsPause() ) { return; }
        rotateCamera();
        moveCamera();
	}

    /// <summary> Ruch postaci po określonej ścieżce z Waypointów </summary>
    public void moveCamera() {
        if ( !objectPrecisionPositioning( transform, points[current_position], 0.1f ) ) {
            var pos = Vector3.MoveTowards( transform.position, points[current_position], speed * Time.deltaTime );
            GetComponent<Rigidbody>().MovePosition(pos);
        } else {
            if (infinite_loop) {
                current_position = (current_position + 1) % points.Length;
            } else { 
                if (current_position < points.Length-1) { current_position++; }
            }
        }
    }

    /// <summary> Obrót postaci w kierunku następnego Waypointa </summary>
    public void rotateCamera() {
        Vector3 player = transform.position;
        Vector3 target = points[current_position];
        player.y = 0;
        target.y = 0;
        var direction = (target - player).normalized;
        var lookRotation = Quaternion.LookRotation(direction);
        transform.rotation = Quaternion.Slerp( transform.rotation, lookRotation, rotation * Time.deltaTime );
    }

    /// <summary> Sprawdzenie czy postać (obiekt) znajduje się w granicach punktu docelowego </summary>
    /// <param name="subject"> Obiekt, postać </param>
    /// <param name="target"> Obiekt, punkt docelowy </param>
    /// <param name="accuracy"> Tolerancja odległości </param>
    /// <returns> Zwraca informacje czy obiekt jest już w polu docelowym </returns>
    public bool objectPrecisionPositioning( Transform subject, Vector3 target, float accuracy ) {
		float	subjectX		=	subject.position.x;
		float	subjectZ		=	subject.position.z;
		float	targetX			=	target.x;
		float	targetZ			=	target.z;

		bool	positioningX	=	( subjectX < targetX + accuracy && subjectX > targetX - accuracy ) ? true : false;
		bool	positioningZ	=	( subjectZ < targetZ + accuracy && subjectZ > targetZ - accuracy ) ? true : false;
		return	( positioningX && positioningZ );
	}

}
