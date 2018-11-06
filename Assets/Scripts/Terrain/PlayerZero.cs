using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerZero : MonoBehaviour {

    [SerializeField]
    private TerrainController terrainController;

    [SerializeField]
    private float distance = 20;

    private void Update() {

        Camera m_camera = Camera.main;
        transform.position = m_camera.transform.position;

    }

}