using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MotherBallon : MonoBehaviour {
 
    public GameObject prefabSpawn;  // Klonowany prefab oraz cel balonów
    public int amountSpawn = 3;
    public float additionalDistance = 1f;
    bool isCreated = false;
	
    /// <summary> Akcja wykonywana podczas niszczenia GameObjectu </summary>
    void OnDestroy() {
        SpawnBallonPrefab( amountSpawn );
    }

    /// <summary> Funkcja Update </summary>
	void Update () {
        //SpawnBallonPrefab();
	}

    /// <summary> Tworzenie obiektów balonów wokół balonu </summary>
    /// <param name="count"> Ilość tworzonych obiektó </param>
    void SpawnBallonPrefab( int count ) {

        Vector3 center = transform.position;
        float r_size = count > 4 ? count-2 : 1;
        float r = GetComponent<CapsuleCollider>().radius * 2 + (prefabSpawn.GetComponent<CapsuleCollider>().radius * r_size) + additionalDistance;
        float anglePos = ( 360f / count ) * Mathf.PI / 180f;

        if (!isCreated) {
            for (int i = 0; i < count; i++) {
                Vector3 position = new Vector3(
                    center.x + r * Mathf.Cos( i * anglePos ),
                    center.y,
                    center.z + r * Mathf.Sin( i * anglePos )
                );
                GameObject go = (GameObject) Instantiate( prefabSpawn, transform.parent );

                go.name = "Balloon";
                go.transform.position = position;
                go.GetComponent<BalloonBehaviour>().target = GetComponent<BalloonBehaviour>().target;
            }
                        
            isCreated = true;
        }
    }
}
