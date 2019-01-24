using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossBehaviour : MonoBehaviour {

    public Transform target;
    public float speed = 1f;
    public int current_position;
    private List<GameObject> collision_list;

    public GameObject progress;
    private float p_width;

    public int max_lives = 20;
    private int lives;
    public int give_points = 1;
    public int take_lives = 1;

    private bool after_hit = false;
    public float await_hit = 2.0f;
    private float timer = 0f;

    /// <summary> Funkcja Inicjalizacji </summary>
    void Start() {
        collision_list = new List<GameObject>();
        p_width = progress.transform.localScale.x;
        lives = max_lives;
        RemoveLives(10);
    }

    /// <summary> Funkcja Update </summary>
    void Update() {
        if ( GamePause.IsPause() ) {
            StopBallon();
            return;
        }

        if ( !after_hit ) {
            MovingBallon();
            timer = 0;
        } else {
            timer += Time.deltaTime;
            if ( timer > await_hit ) {
                after_hit = false;
            }
        }
    }

    /// <summary> Wykrycie kolizji z innym obiektem </summary>
    /// <param name="collision"> Collider innego obiektu </param>
    void OnCollisionEnter ( Collision collision ) {
        Debug.Log( "Collision detected by: " + this.name + " with: " + collision.gameObject.name );
        collision_list.Add( collision.gameObject );

        if ( collision.gameObject.name == "CameraRig" ) {
            collision.gameObject.GetComponent<Player>().RemoveLives( take_lives );
            collision.gameObject.GetComponent<PlayEffect>().Play( 1 );
            Destroy( this.gameObject );

        } else {
            foreach ( GameObject collisionObject in collision_list ) {
                if ( collisionObject == null ) {
                    collision_list.Remove( collisionObject );
                    return;
                }

                if( collisionObject.name == "CameraRig" ) {
                    collisionObject.GetComponent<Player>().RemoveLives( take_lives );
                    collisionObject.GetComponent<PlayEffect>().Play( 1 );
                    after_hit = true;
                    StopBallon();
                }
            }
        }
    }

    /// <summary> Wykrycie ukończenia kolizji z innym obiektem </summary>
    /// <param name="collision"> Collider innego obiektu </param>
    private void OnCollisionExit( Collision collision ) {
        GameObject collisionObject = collision.gameObject;
        if ( collision_list.Contains( collisionObject ) ) {
            collision_list.Remove( collisionObject );
        }
    }

    /// <summary> Ruch obiektu do określonego miejsca </summary>
    public void MovingBallon() {
        if ( !ObjectPrecisionPositioning( transform, target, 0.1f ) ) {
            var pos = Vector3.MoveTowards(transform.position, target.position, speed * Time.deltaTime);
            GetComponent<Rigidbody>().MovePosition(pos);
        }
    }

     /// <summary> Zatrzymanie obiektu </summary>
    public void StopBallon() {
        GetComponent<Rigidbody>().velocity = Vector3.zero;
    }

    /// <summary> Funkcja odejmująca życie bossowi. </summary>
    /// <param name="live"></param>
    public void RemoveLives(int live) {
        if ( lives <= 0 ) { Destroy(this); }
        lives -= live;
        CalculateProgress();
    }

    /// <summary> Funkcja pokazująca życie bosa na panelu progress. </summary>
    public void CalculateProgress() {
        float new_width = (lives * p_width)/max_lives;
        float new_position = (p_width - new_width)/2;
        Debug.Log(new_position);

        Vector3 scale = new Vector3(
            new_width,
            progress.transform.localScale.y,
            progress.transform.localScale.z
        );
        progress.transform.localScale = scale;
        
        //Vector3 position = new Vector3( new_position, 0, 0 );
        //progress.transform.position = position;
    }

    /// <summary> Sprawdzenie czy postać (obiekt) znajduje się w granicach punktu docelowego </summary>
    /// <param name="subject"> Aktualna pozycja obiektu przemieszczającego się </param>
    /// <param name="target"> Położenie punktu docelowego </param>
    /// <param name="accuracy"> Tolerancja odległości </param>
    /// <returns> Zwraca informacje czy obiekt jest już w polu docelowym </returns>
    public static bool ObjectPrecisionPositioning( Transform subject, Transform target, float accuracy ) {
        float subjectX = subject.position.x;
        float subjectY = subject.position.y;
        float subjectZ = subject.position.z;
        float targetX = target.position.x;
        float targetY = target.position.y;
        float targetZ = target.position.z;

        bool positioningX = (subjectX < targetX + accuracy && subjectX > targetX - accuracy) ? true : false;
        bool positioningY = (subjectY < targetY + accuracy && subjectY > targetY - accuracy) ? true : false;
        bool positioningZ = (subjectZ < targetZ + accuracy && subjectZ > targetZ - accuracy) ? true : false;
        return (positioningX && positioningY && positioningZ);
    }

}
