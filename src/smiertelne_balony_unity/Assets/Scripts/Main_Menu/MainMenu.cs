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

    public string[,] menu_info = new string[,] {
        { "Fabuła\ni Narracja", "Fabuła i narracja\nLaura Dymarczyk" },
        { "Twórca\nAssetów", "Twórca Obiektów 3D\nAgata Dziurka" },
        { "Programista\n1", "Programista 1\n Kamil Karpiński" },
        { "Programista\n2", "Programista 1\n Adrian Rupala" }
    };

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

            } else if ( raycastObject.name == "Levels Balloon" ) {
                // ... // ... // ... //

            } else if ( raycastObject.name == "Exit Balloon" ) {
                Application.Quit();

            } else if ( raycastObject.name == "Info_AD Balloon" ) {
                ChangeTexts( raycastObject, 0, true );

            } else if ( raycastObject.name == "Info_LD Balloon" ) {
                ChangeTexts( raycastObject, 1, true );

            } else if ( raycastObject.name == "Info_KK Balloon" ) {
                ChangeTexts( raycastObject, 2, true );

            } else if ( raycastObject.name == "Info_AR Balloon" ) {
                ChangeTexts( raycastObject, 3, true );

            } else {
                for ( int i = 0; i < 4; i++ ) {
                    ChangeTexts( raycastObject, i, true );
                }
            }
        }
	}

    /// <summary> Zmień tekst balona na podstawie tablicy publicznej </summary>
    /// <param name="obj"> Obiekt balonu </param>
    /// <param name="textID"> Indeks tablicy zewnętrznej </param>
    /// <param name="show"> Prawda dla tekstu rozwiniętego </param>
    private void ChangeTexts( GameObject obj, int textID, bool show ) {
        var child = obj.transform.GetChild(0);
        TextMesh text_mesh = child.GetComponent<TextMesh>();
        text_mesh.text = show ? menu_info[textID,1] : menu_info[textID,0];
    }
}
