using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MakeBigger : MonoBehaviour {

    public Transform target;    // Gracz
    public float distance = 5f;
    public Vector3 default_size = new Vector3( 1f, 1f, 1f );
    public Vector3 size_speed = new Vector3( 1f, 1f, 1f );

    /// <summary> Funkcja Update </summary>
    void Update () {
        ChangeSizeBasedOnCamera();
    }

    /// <summary> Funkcja powiększająca lub pomniejszająca obiekt na bazie odległości </summary>
    void ChangeSizeBasedOnCamera() {
        // Odległość pomiędzy celem a balonem
        float dist = Vector3.Distance(target.position, transform.position);

        // Jeśli odległość większa niż "distance" zmień rozmiar balona na początkowy "default_size"
        if ( dist > distance ){
            if ( transform.localScale.x < default_size.x || transform.localScale.y <  default_size.y || transform.localScale.z <  default_size.z ) {
                transform.localScale = default_size;
                return;
            }
            transform.localScale -= (size_speed * Time.deltaTime);
        // Jeśli odległość jest mniejsza lub równa dystansowi
        } else if ( dist <= distance ){
            transform.localScale += (size_speed * Time.deltaTime);
        }
    }
}
