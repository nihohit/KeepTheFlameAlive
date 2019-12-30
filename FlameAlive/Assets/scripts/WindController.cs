using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WindController : MonoBehaviour {
    public float distanceToReset;
    public GameObject healthObject;
    private UnityEngine.UI.Slider slider;
    int numberOfClouds;
    float[] speedCoefficient;
    Vector3 windDirection = new Vector3(0, 0, 5);
    Vector3 previousMousePosition = Vector3.zero;
    AudioSource wind;
    float pitchMin = 0.8f;
    float pitchMax = 1.2f;
    float volumeMin = 0.8f;
    float volumeMax = 1.1f;
    float timeToIgnorePress = 1f;
    float ignorePress;
    public AudioClip[] winds;
    int index = 0;

    // Start is called before the first frame update
    void Start() {
        slider = healthObject.GetComponent<UnityEngine.UI.Slider>();
        numberOfClouds = transform.childCount;
        speedCoefficient = new float[numberOfClouds];
        for (int i = 0; i < numberOfClouds; i++) {
            resetCloud(i, UnityEngine.Random.Range(distanceToReset, 2 * distanceToReset));
        }
        wind = GetComponent<AudioSource>();
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
                resetCloud(i, distanceToReset);
            }
        }

        var mousePosition = new Vector3(Input.mousePosition.x, 0, Input.mousePosition.y);
        if (Input.GetMouseButton(0) && ignorePress + timeToIgnorePress < Time.time) {
            var difference = (mousePosition - previousMousePosition);
            difference.x /= Screen.width;
            difference.z /= Screen.height;
            windDirection += difference / Time.deltaTime * (slider.value > 20 ? 1f : 0.1f);
            slider.value -= 100 * Time.deltaTime;
            if (!wind.isPlaying) {
                wind.pitch = UnityEngine.Random.Range(pitchMin, pitchMax);
                wind.volume = UnityEngine.Random.Range(volumeMin, volumeMax);
                wind.clip = winds[index];
                index++;
                if (index == winds.Length) {
                    index = 0;
                }
                wind.Play();
            }
            if (slider.value <= 0) {
                ignorePress = Time.time;
            }
        } else {
            slider.value += 40 * Time.deltaTime;
            var strength = Vector3.Distance(windDirection, Vector3.zero);
            var tooHigh = strength > 7;
            var tooLow = strength < 5;
            if (tooHigh || tooLow) {
                var rateOfChange = 1.5f;
                var xChange = UnityEngine.Random.Range(0f, rateOfChange);
                var zChange = rateOfChange - xChange;
                if (tooHigh) {
                    windDirection.x -= (windDirection.x > 0 ? xChange : -xChange) * Time.deltaTime;
                    windDirection.z -= (windDirection.z > 0 ? zChange : -zChange) * Time.deltaTime;
                } else {
                    windDirection.x += (windDirection.x > 0 ? xChange : -xChange) * Time.deltaTime;
                    windDirection.z += (windDirection.z > 0 ? zChange : -zChange) * Time.deltaTime;
                }

            }
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