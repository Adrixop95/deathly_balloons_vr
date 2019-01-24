using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlaySound : MonoBehaviour {

    public  bool            active      =   true;
    public  float           distance    =   15f;
    [Range(0,1)]
    public  float           random      =   0.5f;
    public  float           timeDist    =   20f;
    public  float           initVolume  =   0.5f;
    public  GameObject      target;
    public  AudioClip[]     source;

    private bool            played      =   false;
    private AudioSource     audioCom;
    private float           timer       =   0f;

    // ################################################################################
    void Start() {
        if ( GetComponent<AudioSource>() ) { audioCom = GetComponent<AudioSource>(); }
        else { audioCom = gameObject.AddComponent(typeof(AudioSource)) as AudioSource; }

        timer               =   timeDist;
        audioCom.volume     =   initVolume;
        audioCom.loop       =   false;
    }

    // --------------------------------------------------------------------------------
    void Update() {
        if ( !active ) { return; }
        if ( audioCom.isPlaying ) { return; }
        if ( target == null ) { return; }

        float   distance    =   Vector3.Distance( target.transform.position, transform.position );
        if ( distance > this.distance ) {
            played  =   false;
            return;
        }

        if ( Await() ) { return; }
        float   randomVal   =   Random.Range( 0f, 1f );
        if ( randomVal <= random ) {
            timer = 0f;
            Play();
        }
    }

    // ################################################################################
    public void Play() {
        played  =   true;
        if ( !audioCom.isPlaying ) {
            if ( source.Length <= 0 ) { return; }
            audioCom.clip   =   source[ Random.Range( 0, source.Length ) ];
            audioCom.Play();
        }
    }

    // --------------------------------------------------------------------------------
    public void Stop() {
        if ( audioCom.isPlaying ) { audioCom.Stop(); }
    }

    // --------------------------------------------------------------------------------
    public void SetVolume( float newVolume ) {
        audioCom.volume     =   newVolume;
    }

    // --------------------------------------------------------------------------------
    public bool Await() {
        timer   +=  Time.deltaTime;
        if ( timer < timeDist ) { return true; }
        return false;
    }

    // ################################################################################
    public void SetActive( bool active ) {
        this.active =   active;
        if ( !active ) { Stop(); }
    }

    // ################################################################################
}
