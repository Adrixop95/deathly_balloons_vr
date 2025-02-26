﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour {

    [TextArea(3,10)]
    public string win_text = "Game Over\nScore: ";
    [TextArea(3,10)]
    public string loose_text = "Game Over\nScore: ";

    public Transform ballons_container;
    private Menu main_menu;
    private TextMesh final_text;

    public TextMesh points_mesh;
    public string points_text;
    public TextMesh lives_mesh;
    public string lives_text;

    public int lives = 3;
    private int lives_detector;
    private int points = 0;
    private int points_detector;

	/// <summary> Funkcja Inicjalizacji </summary>
	void Start () {
		main_menu = transform.Find("Interior UI").GetComponent<Menu>();
        final_text = main_menu.GetFinalText().GetComponent<TextMesh>();
        points = 0;
	}
	
	/// <summary> Funkcja Update </summary>
	void Update () {
        if ( GamePause.IsPause() ) { return; }

        if (lives != lives_detector ) {
            lives_detector = lives;
            lives_mesh.text = lives_text + lives.ToString();
        }

        if (points != points_detector) {
            points_detector = points;
            points_mesh.text = points_text + points.ToString();
        }

        CheckWin();
        CheckLoose();
	}

    /// <summary> Funkcja sprawdzająca wygraną </summary>
    private void CheckWin() {
        if ( ballons_container.childCount <= 0 ) {
            Debug.Log("Win Detected");
            final_text.text = win_text + points.ToString();
            main_menu.ShowPauseButton( false );
            main_menu.OpenFinal( true );
            main_menu.SetNext( "Następny poziom", true );
            GamePause.Pause();
        }
    }

    /// <summary> Funkcja sprawdzająca przegraną </summary>
    private void CheckLoose() {
        if ( lives <= 0 ) {
            Debug.Log("Loose Detected");
            GameObject.Find( "Map" ).GetComponent<PlayMusic>().Activate( false );
            GetComponent<PlayEffect>().Play( 0 );
            final_text.text = loose_text + points.ToString();
            main_menu.ShowPauseButton( false );
            main_menu.OpenFinal( true );
            main_menu.SetNext( "Powtórz poziom", false );
            GamePause.Pause();
        }
    }

    /// <summary> Funkcja dodająca punkty </summary>
    /// <param name="points"> punkty </param>
    public void AddPoints( int points ) { this.points += points; }

    /// <summary> Funkcja dodająca punkty życia </summary>
    /// <param name="lives"> punkty życia </param>
    public void AddLives( int lives ) { this.lives += lives; }

    /// <summary> Funkcja odejmująca punkty życia </summary>
    /// <param name="points"> punkty życia </param>
    public void RemoveLives( int lives ) { this.lives -= lives; }
}
