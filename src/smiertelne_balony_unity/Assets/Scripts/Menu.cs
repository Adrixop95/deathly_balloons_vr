using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Menu : MonoBehaviour {

    public CameraRaycast raycast;
    public bool start_pause = true;
    public float raycast_time = 5f;
    private GameObject mainmenu;
    private GameObject pausebutton;
    private GameObject pausemenu;
    private GameObject finalmenu;

	/// <summary> Funkcja Inicjalizacji </summary>
	void Start () {
        pausebutton = transform.GetChild(0).gameObject;
		mainmenu = transform.GetChild(1).gameObject;
        pausemenu = transform.GetChild(2).gameObject;
        finalmenu = transform.GetChild(3).gameObject;

        if ( start_pause ) { GamePause.Pause(); }
	}
	
	/// <summary> Funkcja Update (Wykrycie elementu menu i wykonanie akcji po określonym upływie czasu) </summary>
	void Update () {
		if ( raycast.GetSelectedActive() >= raycast_time ) {
            GameObject raycastObject = raycast.GetSelectedObject();
            if ( raycastObject == null ) { return; }
            if ( raycastObject.name == "Pause Button" ) {
                GamePause.Pause();
                OpenPause( true );

            } else if ( raycastObject.name == "Start Button" ) {
                GamePause.Resume();
                OpenStart( false );
                ShowPauseButton( true );

            } else if ( raycastObject.name == "Resume Button" ) {
                GamePause.Resume();
                OpenPause( false );
                ShowPauseButton( true );

            } else if ( raycastObject.name == "Exit Button" ) {
                Application.Quit();
            }
        }
	}

    /// <summary> Otwiera bądź zamyka menu główne </summary>
    /// <param name="open"> Otwarte/Zamknięte </param>
    public void OpenStart( bool open ) {
        mainmenu.SetActive( open );
    }

    /// <summary> Otwiera bądź zamyka menu pauzy </summary>
    /// <param name="open"> Otwarte/Zamknięte </param>
    public void OpenPause( bool open ) {
        pausemenu.SetActive( open );
    }

    /// <summary> Otwiera bądź zamyka menu zakończenia rozgrywki </summary>
    /// <param name="open"> Otwarte/Zamknięte </param>
    public void OpenFinal( bool open ) {
        finalmenu.SetActive( open );
    }

    /// <summary> Pokazuje lub ukrywa przycisk pauzy </summary>
    /// <param name="open"> Pokazany/Ukryty </param>
    public void ShowPauseButton( bool show ) {
        pausebutton.SetActive( show );
    }

    /// <summary> Pobiera komponent tekstowy menu finałowego </summary>
    /// <returns> TextMesh menu finałowego </returns>
    public TextMesh GetFinalText() {
        return finalmenu.transform.Find("EndMenu Title").GetComponent<TextMesh>();
    }
}
