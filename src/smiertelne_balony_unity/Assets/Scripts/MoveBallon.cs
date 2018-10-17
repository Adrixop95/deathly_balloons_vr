using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveBallon : MonoBehaviour
{

    public Transform[] waypoints;
    public float speed = 1f;
    public float rotation = 10f;
    public int current_position;

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        moveCamera();
    }

    void OnTriggerEnter(Collider collision)
    {
        if (collision.gameObject.name == "Camera") {
            Destroy(this.gameObject);
        }
    }

        /*
    void OnCollisionEnter(Collision col)
    {
        if (col.gameObject.name == "Camera")
        {
            Destroy(this.gameObject);
        }
    }
    */

    public void moveCamera()
    {
        if (!objectPrecisionPositioning(transform, waypoints[current_position], 0.1f))
        {
            var pos = Vector3.MoveTowards(transform.position, waypoints[current_position].position, speed * Time.deltaTime);
            GetComponent<Rigidbody>().MovePosition(pos);
        }
        else
        {
            current_position = (current_position + 1) % waypoints.Length;
        }

    }

    public static bool objectPrecisionPositioning(Transform subject, Transform target, float accuracy)
    {
        float subjectX = subject.position.x;
        float subjectZ = subject.position.z;
        float targetX = target.position.x;
        float targetZ = target.position.z;

        bool positioningX = (subjectX < targetX + accuracy && subjectX > targetX - accuracy) ? true : false;
        bool positioningZ = (subjectZ < targetZ + accuracy && subjectZ > targetZ - accuracy) ? true : false;
        return (positioningX && positioningZ);
    }

}
