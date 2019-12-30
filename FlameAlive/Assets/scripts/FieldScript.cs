using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FieldScript : MonoBehaviour {
    public GameObject textObject;
    public GameObject growable;
    public string headerText;
    public int gainPerUpdate;
    public int lossPerUpdate;
    private UnityEngine.UI.Text textField;
    private int numberOfCollidingClouds = 0;
    private float score;

    // Start is called before the first frame update
    void Start() {
        textField = textObject.GetComponent<UnityEngine.UI.Text>();
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
        GameObject.Instantiate(Resources.Load("Cultist"), growable.transform.position, Quaternion.identity);
    }

    void OnTriggerEnter(Collider other) {
        numberOfCollidingClouds++;
    }

    void OnTriggerExit(Collider other) {
        numberOfCollidingClouds--;
    }
}