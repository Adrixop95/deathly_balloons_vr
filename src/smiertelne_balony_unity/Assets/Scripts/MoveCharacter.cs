using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveCharacter : MonoBehaviour {

    public Transform path;
    public float speed = 1f;
    public float rotation = 1f;
    public int current_position;
    public bool infinite_loop = true;

	/// <summary> Funkcja Inicjalizacji </summary>
	void Start () {
		
	}
	
	/// <summary> Funkcja Update </summary>
	void Update () {
        rotateCamera();
        moveCamera();
	}

    /// <summary> Ruch postaci po określonej ścieżce z Waypointów </summary>
    public void moveCamera() {
        if ( !objectPrecisionPositioning( transform, path.GetChild(current_position), 0.1f ) ) {
            var pos = Vector3.MoveTowards( transform.position, path.GetChild(current_position).position, speed * Time.deltaTime );
            GetComponent<Rigidbody>().MovePosition(pos);
        } else {
            if (infinite_loop) {
                current_position = (current_position + 1) % path.childCount;
            } else { 
                if (current_position < path.childCount-1) { current_position++; }
            }
        }
    }

    /// <summary> Obrót postaci w kierunku następnego Waypointa </summary>
    public void rotateCamera() {
        Vector3 player = transform.position;
        Vector3 target = path.GetChild(current_position).position;
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
    public bool objectPrecisionPositioning( Transform subject, Transform target, float accuracy ) {
		float	subjectX		=	subject.position.x;
		float	subjectZ		=	subject.position.z;
		float	targetX			=	target.position.x;
		float	targetZ			=	target.position.z;

		bool	positioningX	=	( subjectX < targetX + accuracy && subjectX > targetX - accuracy ) ? true : false;
		bool	positioningZ	=	( subjectZ < targetZ + accuracy && subjectZ > targetZ - accuracy ) ? true : false;
		return	( positioningX && positioningZ );
	}

    public bool objectPrecisionRotating( Vector3 subject, Vector3 target, float accuracy ) {
		float	subjectX		=	subject.x;
		float	subjectZ		=	subject.z;
		float	targetX			=	target.x;
		float	targetZ			=	target.z;

		bool	positioningX	=	( subjectX < targetX + accuracy && subjectX > targetX - accuracy ) ? true : false;
		bool	positioningZ	=	( subjectZ < targetZ + accuracy && subjectZ > targetZ - accuracy ) ? true : false;
		return	( positioningX && positioningZ );
    }

}
