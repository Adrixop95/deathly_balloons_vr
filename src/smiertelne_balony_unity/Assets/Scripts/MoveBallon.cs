using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveBallon : MonoBehaviour {

    public Transform target;
    public float speed = 1f;
    public int current_position;

    /// <summary> Funkcja Inicjalizacji </summary>
    void Start() {

    }

    /// <summary> Funkcja Update </summary>
    void Update() {
        moveBallon();
    }

    /// <summary> Wykrycie kolizji i innym obiektem </summary>
    /// <param name="collision"> Collider innego obiektu </param>
    void OnTriggerEnter(Collider collision) {
        if (collision.gameObject.name == "Camera") {
            Destroy(this.gameObject);
        }
    }

    /// <summary> Ruch obiektu do określonego miejsca </summary>
    public void moveBallon() {
        if (!objectPrecisionPositioning(transform, target, 0.1f)) {
            var pos = Vector3.MoveTowards(transform.position, target.position, speed * Time.deltaTime);
            GetComponent<Rigidbody>().MovePosition(pos);
        }
    }

    /// <summary> Sprawdzenie czy postać (obiekt) znajduje się w granicach punktu docelowego </summary>
    /// <param name="subject"> Obiekt, postać </param>
    /// <param name="target"> Obiekt, punkt docelowy </param>
    /// <param name="accuracy"> Tolerancja odległości </param>
    /// <returns> Zwraca informacje czy obiekt jest już w polu docelowym </returns>
    public static bool objectPrecisionPositioning(Transform subject, Transform target, float accuracy) {
        float subjectX = subject.position.x;
        float subjectZ = subject.position.z;
        float targetX = target.position.x;
        float targetZ = target.position.z;

        bool positioningX = (subjectX < targetX + accuracy && subjectX > targetX - accuracy) ? true : false;
        bool positioningZ = (subjectZ < targetZ + accuracy && subjectZ > targetZ - accuracy) ? true : false;
        return (positioningX && positioningZ);
    }

}
