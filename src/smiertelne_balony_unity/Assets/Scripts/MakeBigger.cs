using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MakeBigger : MonoBehaviour {

    // nasz cel
    public Transform target;

    void Start () {
    }

    void Update () {
        changeSizeBasedOnCamera();
    }

    void changeSizeBasedOnCamera() {
         
        // odległość pomiędzy celem a balonem
        float dist = Vector3.Distance(target.position, transform.position);

        // jeśli odległość większa niż 5f zmień rozmiar balona na początkowy (1f)
        // jeśli odległość jest mniejsza lub równa 3f zwiększaj rozmiar balona o 0.01
        // wymagany balans
        if (dist > 5f){
            transform.localScale = new Vector3(1f, 1f, 1f);
        } else if (dist <= 4.3f){
            transform.localScale += new Vector3(0.01f, 0.01f, 0.01f);
        }
    }
}
