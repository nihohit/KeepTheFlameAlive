using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireScript : MonoBehaviour {
    public int gainPerUpdate;
    public int lossPerUpdate;
    public GameObject healthObject;
    public GameObject endGameScreen;
    private UnityEngine.UI.Slider slider;
    private int numberOfCollidingClouds = 0;
    private int score;
    private AudioSource sizzle;
    private AudioSource chant;
    int cultistCount = 0;

    // Start is called before the first frame update
    void Start() {
        slider = healthObject.GetComponent<UnityEngine.UI.Slider>();
        score = (int) slider.value;
        var audios = GetComponents<AudioSource>();
        sizzle = audios[0];
        chant = audios[1];
        chant.volume = 0;
    }

    // Update is called once per frame
    void Update() {
        score += (numberOfCollidingClouds > 0) ? -lossPerUpdate : gainPerUpdate;
        if (numberOfCollidingClouds > 0) {
            sizzle.mute = false;
        } else {
            sizzle.mute = true;
        }
        score = Math.Min(score, (int) slider.maxValue);
        slider.value = score;
        if (score <= 0) {
            endGameScreen.SetActive(true);
            endGameScreen.GetComponentInChildren<UnityEngine.UI.Text>().text = "You LOSE :(";
        }
    }

    public void reportEntered() {
        ++cultistCount;
        if (cultistCount == 15) {
            endGameScreen.SetActive(true);
            endGameScreen.GetComponentInChildren<UnityEngine.UI.Text>().text = "You WIN :) :) :)";
        }
        chant.volume = 0.2f + (((float) cultistCount) / 15 * 0.8f);
    }

    void OnTriggerEnter(Collider other) {
        numberOfCollidingClouds++;
    }

    void OnTriggerExit(Collider other) {
        numberOfCollidingClouds--;
    }
}