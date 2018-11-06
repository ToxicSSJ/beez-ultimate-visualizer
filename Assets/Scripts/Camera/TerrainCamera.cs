using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TerrainCamera : MonoBehaviour {
	
	void Update () {

        foreach (Transform child in transform) {
            child.position = transform.position;
        }

    }
}
