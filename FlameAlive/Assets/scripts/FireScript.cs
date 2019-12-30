using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireScript : MonoBehaviour {
    public int gainPerUpdate;
    public int lossPerUpdate;
    public GameObject healthObject;
    private UnityEngine.UI.Slider slider;
    private int numberOfCollidingClouds = 0;
    private int score;

    // Start is called before the first frame update
    void Start() {
        slider = healthObject.GetComponent<UnityEngine.UI.Slider>();
        score = (int) slider.value;
    }

    // Update is called once per frame
    void Update() {
        score += (numberOfCollidingClouds > 0) ? -lossPerUpdate : gainPerUpdate;
        score = Math.Min(score, (int) slider.maxValue);
        slider.value = score;
    }

    void OnTriggerEnter(Collider other) {
        numberOfCollidingClouds++;
    }

    void OnTriggerExit(Collider other) {
        numberOfCollidingClouds--;
    }
}