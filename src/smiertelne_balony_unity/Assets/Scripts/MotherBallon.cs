using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MotherBallon : MonoBehaviour {

    // Pobranie klonowanego prefabu oraz celu balonów
    public GameObject prefabSpawn;
    public Transform target;

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
            for (int i = 0; i < 2; i++) {
                // Tworzenie klona z parametrem dziedziczonym z MoveBallon.cs
                GameObject go = (GameObject)Instantiate(prefabSpawn);
                go.GetComponent<MoveBallon>().target = target;
            }
                        
            isCreated = true;
        }
    }
}
