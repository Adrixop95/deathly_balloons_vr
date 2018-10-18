using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Menu : MonoBehaviour {

    public CameraRaycast raycast;
    public bool start_pause = true;
    private GameObject mainmenu;
    private GameObject menubutton;
    private GameObject pausemenu;

	// Use this for initialization
	void Start () {
        menubutton = transform.GetChild(0).gameObject;
		mainmenu = transform.GetChild(1).gameObject;
        pausemenu = transform.GetChild(2).gameObject;

        if ( start_pause ) { GamePause.Pause(); }
	}
	
	// Update is called once per frame
	void Update () {
		if ( raycast.GetSelectedActive() ) {
            GameObject raycastObject = raycast.GetSelectedObject();
            if ( raycastObject == null ) { return; }
            if ( raycastObject.name == "MainMenu Button" ) {
                GamePause.Pause();
                menubutton.SetActive( false );
                pausemenu.SetActive( true );

            } else if ( raycastObject.name == "Start Button" ) {
                GamePause.Resume();
                menubutton.SetActive( true );
                mainmenu.SetActive( false );

            } else if ( raycastObject.name == "Resume Button" ) {
                GamePause.Resume();
                menubutton.SetActive( true );
                pausemenu.SetActive( false );

            } else if ( raycastObject.name == "Exit Button" ) {
                Application.Quit();

            }
        }
	}
}
