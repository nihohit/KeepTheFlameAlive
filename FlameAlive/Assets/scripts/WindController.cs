﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WindController : MonoBehaviour {
    public float distanceToReset;
    int numberOfClouds;
    float[] speedCoefficient;
    Vector3 windDirection = new Vector3(0, 0, 5);
    Vector3 previousMousePosition = Vector3.zero;

    // Start is called before the first frame update
    void Start() {
        numberOfClouds = transform.childCount;
        speedCoefficient = new float[numberOfClouds];
        for (int i = 0; i < numberOfClouds; i++) {
            resetCloud(i, UnityEngine.Random.Range(-distanceToReset, distanceToReset));
        }
        // Debug.Break();
    }

    // Update is called once per frame
    void Update() {
        var windChange = windDirection * Time.deltaTime;
        for (int i = 0; i < numberOfClouds; ++i) {
            var cloud = transform.GetChild(i);
            var position = cloud.transform.position;
            var oldDistance = Vector3.Distance(position, Vector3.zero);
            position += windChange * speedCoefficient[i];
            var newDistance = Vector3.Distance(position, Vector3.zero);
            cloud.transform.position = position;
            if (newDistance > distanceToReset && newDistance > oldDistance) {
                resetCloud(i, 12);
            }
        }

        var mousePosition = new Vector3(Input.mousePosition.x, 0, Input.mousePosition.y);
        if (Input.GetMouseButton(0)) {
            var difference = (mousePosition - previousMousePosition);
            difference.x /= Screen.width;
            difference.z /= Screen.height;
            windDirection += difference / Time.deltaTime;
        }

        previousMousePosition = mousePosition;
    }

    void resetCloud(int i, float distanceToCenter) {
        rerollSpeed(i);
        var cloud = transform.GetChild(i);
        var direction = windDirection;
        direction.Normalize();
        direction *= distanceToCenter;
        var cross = Vector3.Cross(Vector3.up, direction);
        var position = (cross * UnityEngine.Random.Range(-1f, 1f)) - direction;
        position.y = UnityEngine.Random.Range(2f, 4f);
        cloud.transform.localScale = vectorOnRange(0.03f, 0.07f);
        cloud.transform.eulerAngles = new Vector3(0, UnityEngine.Random.Range(0, 180), 0);

        cloud.transform.position = position;
    }

    void rerollSpeed(int i) {
        speedCoefficient[i] = UnityEngine.Random.Range(0.8f, 1.2f);
    }

    Vector3 vectorOnRange(float min, float max) {
        return new Vector3(UnityEngine.Random.Range(min, max), UnityEngine.Random.Range(min, max), UnityEngine.Random.Range(min, max));
    }
}