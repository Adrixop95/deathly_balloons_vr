using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour {

    public CameraRaycast raycaster;
    public bool start_pause = true;
    public float raycast_time = 5f;

    private GameObject new_game;
    private GameObject levels;
    private GameObject exit;
    private GameObject[] info = new GameObject[4];

	// Use this for initialization
	void Start () {
		new_game = transform.GetChild(0).gameObject;
        levels = transform.GetChild(1).gameObject;
        exit = transform.GetChild(2).gameObject;

        for( int i = 0; i < 4; i++ ) {
            info[i] = transform.GetChild(i+3).gameObject;
        }
	}
	
	// Update is called once per frame
	void Update () {
		if ( raycaster.GetSelectedActive() >= raycast_time ) {
            GameObject raycastObject = raycaster.GetSelectedObject();
            if ( raycastObject == null ) { return; }
            if ( raycastObject.name == "Start Balloon" ) {
                SceneManager.LoadScene( "Level_01", LoadSceneMode.Single );

            } else if ( raycastObject.name == "Load Balloon" ) {
                SceneManager.LoadScene( "Level_02", LoadSceneMode.Single );

            } else if ( raycastObject.name == "Exit Balloon" ) {
                Application.Quit();

            }
        }
	}

}
