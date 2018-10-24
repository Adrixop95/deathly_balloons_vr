using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MotherBallon : MonoBehaviour {

    // Pobranie klonowanego prefabu oraz celu balonów
    public GameObject prefabSpawn;

    bool isCreated = false;

    void Start () {
		
	}
	
	void Update () {
        spawnBallonPrefab();
		
	}

    void spawnBallonPrefab() {
        // Sprawdzenie czy został stworzony
        if (!isCreated) {
            // Tworzenie 2 balonów 
            for (int i = 0; i < 3; i++) {
                // Tworzenie klona z parametrem dziedziczonym z MoveBallon.cs
                GameObject go = (GameObject) Instantiate( prefabSpawn );
                //go.GetComponent<CapsuleCollider>().isTrigger = true;

                int minus = ((Random.Range(0, 1) > 0 ? -1 : 1));
                Vector3 adder = new Vector3((1f + Random.Range(0f, 2f)) * minus, (1f + Random.Range(0f, 2f)) * minus, (1f + Random.Range(0f, 2f)) * minus);

                go.transform.position = transform.position + adder;
                go.transform.parent = transform.parent;
                go.GetComponent<MoveBallon>().target = GetComponent<MoveBallon>().target;
            }
                        
            isCreated = true;
        }
    }
}
