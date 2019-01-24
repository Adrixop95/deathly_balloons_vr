using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayMusic : MonoBehaviour {

    public  bool            active      =   true;
    public  bool            shuffle     =   false;
    public  bool            repeat      =   false;
    public  float           timeDist    =   30f;
    public  float           initVolume  =   0.25f;
    public  AudioClip[]     source;

    private AudioSource     audioCom;
    private int             sourceIndex =   -1;
    private float           timer       =   0f;

    // ################################################################################
    void Start() {
        if ( GetComponent<AudioSource>() ) { audioCom = GetComponent<AudioSource>(); }
        else { audioCom = gameObject.AddComponent(typeof(AudioSource)) as AudioSource;}

        audioCom.volume =   initVolume;
        audioCom.loop   =   false;
        timer           =   timeDist;
    }

    // --------------------------------------------------------------------------------
    void Update() {
        if ( !active ) { return; }
        if ( audioCom.isPlaying ) { return; }
        if ( Await() ) { return; }
        Next();
        Play();
    }

    // ################################################################################
    public void Play() {
        if ( audioCom.isPlaying ) { return; }
        if ( audioCom.clip == null ) { return; }
        audioCom.Play();
    }

    // --------------------------------------------------------------------------------
    public void Stop() {
        if ( audioCom.isPlaying ) { audioCom.Stop(); }
    }

    // --------------------------------------------------------------------------------
    public void Next() {
        if ( !repeat ) {
            if ( shuffle ) { sourceIndex = Random.Range( 0, source.Length ); }
            else {
                if ( sourceIndex + 1 < source.Length - 1 ) { sourceIndex++; }
                else { sourceIndex = 0; }
            }
        }
        PlayIndex( sourceIndex );
    }

    // --------------------------------------------------------------------------------
    public void PlayIndex( int index ) {
        if ( source.Length <= 0 ) { return; }
        if ( index < 0 || index > source.Length-1 ) { return; }
        audioCom.clip   =   source[index];
        Play();
    }

    // --------------------------------------------------------------------------------
    public bool Await() {
        timer   +=  Time.deltaTime;
        if ( timer < timeDist ) { return true; }
        timer   =   0;
        return  false;
    }

    // ################################################################################
    public void Activate( bool active ) {
        this.active     =   active;
        if ( !active ) { Stop(); }
    }

    // ################################################################################
}