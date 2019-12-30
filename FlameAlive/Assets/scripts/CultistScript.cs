using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class CultistScript : MonoBehaviour {
    Vector3 initialPosition;
    Vector3 target = new Vector3(0.890671f, 0, 0.403903f);
    float initialDistance;
    float radius = 0.7f;
    float angleChangeSpeed = (2 * Mathf.PI) / 5;

    // Start is called before the first frame update
    void Start() {
        initialPosition = transform.position;
        initialDistance = Vector3.Distance(target, initialPosition);
    }

    // Update is called once per frame
    void Update() {
        var distanceFromTarget = Vector3.Distance(transform.position, target);
        if (distanceFromTarget <= radius) {
            var angleChange = angleChangeSpeed * Time.deltaTime;
            var angle = angleChange + Vector3.Angle(target + Vector3.left, transform.position);
            transform.position = target + new Vector3(Mathf.Cos(angle) * radius, 0, Mathf.Sin(angle) * radius);
        } else {
            var ratio = (initialDistance - distanceFromTarget + Time.deltaTime) / initialDistance;
            transform.position = Vector3.Lerp(initialPosition, target, ratio);
        }
    }
}