using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class CultistScript : MonoBehaviour {
    NavMeshAgent agent;
    // Start is called before the first frame update
    void Start() {
        agent = GetComponent<NavMeshAgent>();
        // agent.
    }

    // Update is called once per frame
    void Update() {
        if (Vector3.Distance(transform.position, Vector3.zero) <= 1.7) {

        } else {
            transform.position = Vector3.Lerp(transform.position, Vector3.zero, Time.deltaTime);
        }
    }
}