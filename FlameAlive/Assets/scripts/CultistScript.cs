using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class CultistScript : MonoBehaviour {
    public FireScript fire;
    Vector3 initialPosition;
    Vector3 target = new Vector3(0.890671f, 0, 0.403903f);
    float initialDistance;
    float radius = 0.7f;
    float angleChangeSpeed;
    bool reported = false;

    // Start is called before the first frame update
    void Start() {
        angleChangeSpeed = angleChangeSpeed = (2 * Mathf.PI) / 5 * Mathf.Rad2Deg;
        initialPosition = transform.position;
        initialDistance = Vector3.Distance(target, initialPosition);
    }

    // Update is called once per frame
    void Update() {
        transform.LookAt(target);
        var distanceFromTarget = Vector3.Distance(transform.position, target);
        if (distanceFromTarget <= radius) {
            if (!reported) {
                fire.reportEntered();
                reported = true;
            }
            transform.RotateAround(target, Vector3.up, angleChangeSpeed * Time.deltaTime);
        } else {
            var ratio = (initialDistance - distanceFromTarget + Time.deltaTime) / initialDistance;
            transform.position = Vector3.Lerp(initialPosition, target, ratio);
        }
    }

    Vector3 difference(float angle) {
        return new Vector3(Mathf.Sin(angle) * radius, 0, Mathf.Cos(angle) * radius);
    }
}