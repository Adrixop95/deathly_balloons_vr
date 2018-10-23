using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveBallon : MonoBehaviour {

    public Transform target;
    public float speed = 1f;
    public int current_position;
    private List<GameObject> collision_list;

    /// <summary> Funkcja Inicjalizacji </summary>
    void Start() {
        collision_list = new List<GameObject>();
    }

    /// <summary> Funkcja Update </summary>
    void Update() {
        if ( GamePause.IsPause() ) {
            StopBallon();
            return;
        }
        MovingBallon();
    }

    /// <summary> Wykrycie kolizji z innym obiektem </summary>
    /// <param name="collision"> Collider innego obiektu </param>
    void OnCollisionEnter ( Collision collision ) {
        Debug.Log( this.name + " :: " + collision.gameObject.name );
        collision_list.Add( collision.gameObject );

        foreach ( GameObject collisionObject in collision_list ) {
            if( collisionObject.name == "CameraRig" ) {
                Destroy( this.gameObject );
            }
        }
    }

    /// <summary> Wykrycie ukończenia kolizji z innym obiektem </summary>
    /// <param name="collision"> Collider innego obiektu </param>
    private void OnCollisionExit( Collision collision ) {
        GameObject collisionObject = collision.gameObject;
        if ( collision_list.Contains( collisionObject ) ) {
            collision_list.Remove( collisionObject );
        }
    }

    /// <summary> Ruch obiektu do określonego miejsca </summary>
    public void MovingBallon() {
        if ( !ObjectPrecisionPositioning( transform, target, 0.1f ) ) {
            var pos = Vector3.MoveTowards(transform.position, target.position, speed * Time.deltaTime);
            GetComponent<Rigidbody>().MovePosition(pos);
        }
    }

     /// <summary> Zatrzymanie obiektu </summary>
    public void StopBallon() {
        GetComponent<Rigidbody>().velocity = Vector3.zero;
    }

    /// <summary> Sprawdzenie czy postać (obiekt) znajduje się w granicach punktu docelowego </summary>
    /// <param name="subject"> Aktualna pozycja obiektu przemieszczającego się </param>
    /// <param name="target"> Położenie punktu docelowego </param>
    /// <param name="accuracy"> Tolerancja odległości </param>
    /// <returns> Zwraca informacje czy obiekt jest już w polu docelowym </returns>
    public static bool ObjectPrecisionPositioning( Transform subject, Transform target, float accuracy ) {
        float subjectX = subject.position.x;
        float subjectY = subject.position.y;
        float subjectZ = subject.position.z;
        float targetX = target.position.x;
        float targetY = target.position.y;
        float targetZ = target.position.z;

        bool positioningX = (subjectX < targetX + accuracy && subjectX > targetX - accuracy) ? true : false;
        bool positioningY = (subjectY < targetY + accuracy && subjectY > targetY - accuracy) ? true : false;
        bool positioningZ = (subjectZ < targetZ + accuracy && subjectZ > targetZ - accuracy) ? true : false;
        return (positioningX && positioningY && positioningZ);
    }

}
