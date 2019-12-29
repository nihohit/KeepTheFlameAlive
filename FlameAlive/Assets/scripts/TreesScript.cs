using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TreesScript : MonoBehaviour {
    // Start is called before the first frame update
    void Start() {
        foreach (Transform child in transform) {
            Debug.Log("o");
            child.eulerAngles = new Vector3(0, UnityEngine.Random.Range(0, 360), 0);
            child.localScale = vectorOnRange(0.04f, 0.1f);
        }
    }

    // Update is called once per frame
    void Update() {

    }

    Vector3 vectorOnRange(float min, float max) {
        var value = UnityEngine.Random.Range(min, max);
        return new Vector3(value, value, value);
    }
}