using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Attack {
    public string name;
    public float lookTime;
    public float attackTime;
    public Attack(string name, float lT, float aT) {
        this.name = name;
        lookTime = lT;
        attackTime = aT;
    }
}

public class FPGun : MonoBehaviour {

    // https://www.youtube.com/watch?v=Ka_cOVSTx6c

    private float timeBetweenBullets = 0.5f;
    private float effectsDisplayTime = 0.5f;
    private float range = 10f;
    private Vector3 destiny;

    public Attack[] attackData = new Attack[] {
        new Attack("Balloon", 0.5f, 1f),
        new Attack("MotherBalloon", 0.5f, 1f),
        new Attack("BigBaloon", 0.5f, 1.5f)
    };

    private float timer;
    private bool isShooting = false;

    private Camera Camera;
    private GameObject gun;
    private LineRenderer gunLine;
    private Player player_data;
    private Transform gunEnd;

    /// <summary> Funkcja Inicjalizacji </summary>
    void Start () {
        Camera = GetComponent<Camera>();
        gun = transform.Find( "pistol" ).gameObject;
        Debug.Log( gun );
        gunEnd = GameObject.FindWithTag("GunEnd").transform;
        gunLine = gunEnd.GetComponent<LineRenderer>();
        range = GetComponent<CameraRaycast>().raycast_distance;
        player_data = transform.parent.GetComponent<Player>();
	}

    /// <summary> Funkcja Update </summary>
	void Update () {
        GameObject target = ObjectDetector();
        float time = TargetingTime();

        bool isObject = false;
        // Sprawdź czy objekt znajduje się na liście obiektów do ataku
        if ( target != null ) {
            foreach ( Attack a in attackData ) {
                if ( target.name == a.name && time >= a.lookTime ) {
                    // Jeżeli patrzy się na niego odpowiednio długo lookTime,
                    // następuje przystępienie do ataku
                    destiny = target.transform.position;
                    isObject = true;
                    // Jeżeli minie czas który wymagany jest aby obiekt został pokonany,
                    // objet zostaje usunięty z pola bitwy
                    if ( time >= a.lookTime + a.attackTime ) {
                        if ( target.GetComponent<BalloonBehaviour>() ) {
                            player_data.AddPoints( target.GetComponent<BalloonBehaviour>().give_points );
                            GameObject.Destroy( target );
                            gun.GetComponent<PlayEffect>().Play( 1 );
                        } else if ( target.GetComponent<BossBehaviour>() ) {
                            target.GetComponent<BossBehaviour>().RemoveLives(1);
                        }
                        isObject = false;
                    }
                    break;
                }
            }
        }

        isShooting = isObject;

        timer += Time.deltaTime;
        if ( isObject && isShooting ) {
            if ( timer < effectsDisplayTime ) { Shoot(); } else { DisableEffects(); }
            if ( timer >= effectsDisplayTime + timeBetweenBullets ) { timer = 0f; }
        } else {
            EndShoot();
            isShooting = false;
        }
	}

    /// <summary> Tymczasowe wyłączenie lasera </summary>
    private void DisableEffects() {
        gunLine.positionCount = 0;
        gunLine.enabled = false;
    }

    /// <summary> Pobiera obiekt na który spogląda kamera </summary>
    /// <returns> Obiekt </returns>
    private GameObject ObjectDetector() {
        return GetComponent<CameraRaycast>().GetSelectedObject();
    }

    /// <summary> Pobranie informacji o czasie spoglądania na obiekt </summary>
    /// <returns> Czas w s </returns>
    private float TargetingTime() {
        return GetComponent<CameraRaycast>().GetSelectedActive();
    }

    /// <summary> Rozpoczęcie ataku, atak ciągły (rysowanie lasera na mapie) </summary>
    private void Shoot() {
        timer = 0f;
        gunLine.enabled = true;
        gun.GetComponent<PlayEffect>().Play( 0 );

        gunLine.positionCount = 2;
        gunLine.SetPosition(0, gunEnd.position);
        gunLine.SetPosition(1, destiny);
    }

    /// <summary> Zakończenie ataku (usunięcie lasera z mapy) </summary>
    private void EndShoot() {
        timer = 0f;
        gunLine.positionCount = 0;
        gunLine.enabled = false;
    }

}
