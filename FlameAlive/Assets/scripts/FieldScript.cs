using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FieldScript : MonoBehaviour {
    public GameObject textObject;
    public FireScript fire;
    public GameObject growable;
    public string headerText;
    public int gainPerUpdate;
    public int lossPerUpdate;
    private UnityEngine.UI.Text textField;
    private int numberOfCollidingClouds = 0;
    private float score;
    private AudioSource popSound;
    float pitchMin = 0.8f;
    float pitchMax = 1.2f;
    float volumeMin = 0.8f;
    float volumeMax = 1.1f;

    // Start is called before the first frame update
    void Start() {
        textField = textObject.GetComponent<UnityEngine.UI.Text>();
        popSound = GetComponentInChildren<AudioSource>();
    }

    // Update is called once per frame
    void Update() {
        score += numberOfCollidingClouds > 0 ? gainPerUpdate : -lossPerUpdate;
        score = Mathf.Max(score, 0f);
        textField.text = headerText + " " + score / 10;
        growable.transform.localScale = new Vector3(score / 100, score / 100, score / 100);
        if (score > 500) {
            popGrowable();
        }
    }

    void popGrowable() {
        score = 0;
        var effect = growable.GetComponentInChildren<ParticleSystem>();
        effect.Play();
        var cultist = (GameObject) GameObject.Instantiate(Resources.Load("Cultist"), growable.transform.position, Quaternion.identity);
        popSound.pitch = UnityEngine.Random.Range(pitchMin, pitchMax);
        popSound.volume = UnityEngine.Random.Range(volumeMin, volumeMax);
        cultist.GetComponent<CultistScript>().fire = fire;
        popSound.Play();
    }

    void OnTriggerEnter(Collider other) {
        numberOfCollidingClouds++;
    }

    void OnTriggerExit(Collider other) {
        numberOfCollidingClouds--;
    }
}