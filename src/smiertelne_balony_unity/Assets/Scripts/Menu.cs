using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Menu : MonoBehaviour {

    public  float       time_look   =   2.5f;
    private float       timer       =   0f;
    private GameObject  last_look   =   null;
    private Ray         camera_look;

    public  GameObject  main_menu;

	/// <summary> Funkcja Inicjalizacji </summary>
	void Start () {

	}
	
	/// <summary> Funkcja Update </summary>
	void Update () {
        var screen_center = new Vector3( Screen.width/2.0f, Screen.height/2.0f, 0 );
		camera_look = Camera.main.ScreenPointToRay( screen_center );
        LookAt( camera_look );
        if ( last_look != null ) { MenuDecision( last_look, timer ); }
	}

    /// <summary> Funkcja sprawdzająca na jaki obiekt spogląda kamera </summary>
    /// <param name="look"> Środkowy punkt kamery </param>
    private void LookAt( Ray look ) {
        RaycastHit result;

        if ( Physics.Raycast(look, out result, 2.0f) ) {
            var object_look = result.transform.gameObject;
            if (last_look != object_look) {
                timer = 0f;
                last_look = object_look;
            }
            timer += Time.deltaTime;
        } else {
            timer = 0f;
            last_look = null;
        }
    }

    /// <summary> Funkcja decyzyjna menu </summary>
    /// <param name="object_look"> Obiekt menu który jest wybierany </param>
    /// <param name="time"> Aktualny czas spoglądania </param>
    private void MenuDecision( GameObject object_look, float time ) {
        if (time < time_look) { return; }
        if ( object_look.name == "MainMenu Button" ) {
            main_menu.SetActive( !main_menu.activeSelf );
            timer = 0f;
            last_look = null;
        }
    }

}
