using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FieldScript : MonoBehaviour {
    public GameObject textObject;
    public string headerText;
    private UnityEngine.UI.Text textField;
    private int numberOfCollidingClouds = 0;
    private long score;

    // Start is called before the first frame update
    void Start() {
        textField = textObject.GetComponent<UnityEngine.UI.Text>();
    }

    // Update is called once per frame
    void Update() {
        score += (numberOfCollidingClouds * 2) - 3;
        textField.text = headerText + " " + score / 10;
    }

    void OnTriggerEnter(Collider other) {
        Debug.Log("collision enter");
        numberOfCollidingClouds++;
    }

    void OnTriggerExit(Collider other) {
        Debug.Log("collision exit");
        numberOfCollidingClouds--;
    }
}