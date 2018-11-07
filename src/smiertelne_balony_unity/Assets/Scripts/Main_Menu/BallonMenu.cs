using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Vector3Static {
    public Vector3Static( Vector3 v3, bool[] vl ) {
        vector3 = v3;
        vector_lock = vl;
    }
    public Vector3 vector3;
    public bool[] vector_lock = new bool[3] { false, true, false };
}

public class BallonMenu : MonoBehaviour {

    public Vector3Static[] points;
    public float speed = 0.1f;
    private int current_position = 0;
	
	void Update () {
        Vector3Static v3s = points[current_position];
        Vector3 target = new Vector3(
            v3s.vector_lock[0] ? v3s.vector3.x : transform.position.x,
            v3s.vector_lock[1] ? v3s.vector3.y : transform.position.y,
            v3s.vector_lock[2] ? v3s.vector3.z : transform.position.z
        );

		if ( !ObjectPrecisionPositioning( transform.position, target, 0.1f ) ) {
            var pos = Vector3.MoveTowards( transform.position, target, speed * Time.deltaTime );
            GetComponent<Rigidbody>().MovePosition(pos);
        } else {
            current_position = (current_position + 1) % points.Length;
        }
	}

    /// <summary> Sprawdzenie czy postać (obiekt) znajduje się w granicach punktu docelowego </summary>
    /// <param name="subject"> Aktualna pozycja obiektu przemieszczającego się </param>
    /// <param name="target"> Położenie punktu docelowego </param>
    /// <param name="accuracy"> Tolerancja odległości </param>
    /// <returns> Zwraca informacje czy obiekt jest już w polu docelowym </returns>
    public static bool ObjectPrecisionPositioning( Vector3 subject, Vector3 target, float accuracy ) {
        float subjectX = subject.x;
        float subjectY = subject.y;
        float subjectZ = subject.z;
        float targetX = target.x;
        float targetY = target.y;
        float targetZ = target.z;

        bool positioningX = (subjectX < targetX + accuracy && subjectX > targetX - accuracy) ? true : false;
        bool positioningY = (subjectY < targetY + accuracy && subjectY > targetY - accuracy) ? true : false;
        bool positioningZ = (subjectZ < targetZ + accuracy && subjectZ > targetZ - accuracy) ? true : false;
        return (positioningX && positioningY && positioningZ);
    }

}
