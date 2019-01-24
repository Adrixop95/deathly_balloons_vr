using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayEffect : MonoBehaviour {

    public  bool        letRestart  =   true;
    public  float       initVolume  =   0.5f;
    public  AudioClip[] source;

    private AudioSource audioCom;

    // ################################################################################
    void Start() {
        if ( GetComponent<AudioSource>() ) { audioCom = GetComponent<AudioSource>(); }
        else { audioCom = gameObject.AddComponent(typeof(AudioSource)) as AudioSource; }

        audioCom.volume =   initVolume;
        audioCom.loop   =   false;
    }

    // ################################################################################
    public void Play( int index ) {
        Debug.Log( index );
        Debug.Log( source.Length );
        if ( index < 0 || index >= source.Length ) { return; }
        if ( audioCom.isPlaying ) {
            if ( !letRestart ) { return; }
            Stop();
        }
        audioCom.clip   =   source[index];
        audioCom.Play();
    }

    // --------------------------------------------------------------------------------
    public void Stop() {
        if ( audioCom.isPlaying ) { audioCom.Stop(); }
    }

    // --------------------------------------------------------------------------------
    public void SetVolume( float newVolume ) {
        audioCom.volume     =   newVolume;
    }

    // ################################################################################
}
