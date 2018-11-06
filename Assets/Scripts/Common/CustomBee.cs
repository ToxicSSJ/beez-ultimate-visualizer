using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CustomBee : MonoBehaviour {

	// Use this for initialization
	void Start () {

    }
	
	// Update is called once per frame
	void Update () {

        /*Vector3 screenPoint = Camera.main.WorldToViewportPoint(transform.position);
        bool onScreen = screenPoint.z > 0 && screenPoint.x > 0 && screenPoint.x < 1 && screenPoint.y > 0 && screenPoint.y < 1;

        if (onScreen) {
            float dist = Vector3.Distance(Camera.main.transform.position, transform.position);
            gameObject.transform.localScale = new Vector3(.5f, .5f, .5f);

            if(dist >= 115) {

                gameObject.transform.localScale = new Vector3(.0f, .0f, .0f);
                return;

            }

            print(dist);

        }*/

    }

    void OnBecameVisible() {
        print("became visible");
        enabled = true;
    }

    void OnBecameInvisible() {
        print("became invisible");
        enabled = false;
    }

}
