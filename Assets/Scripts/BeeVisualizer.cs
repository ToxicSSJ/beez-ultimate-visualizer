using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BeeVisualizer : MonoBehaviour {
    
	void Start () {

        string[] args = System.Environment.GetCommandLineArgs();
        string input = "";
        for (int i = 0; i < args.Length; i++) {
            Debug.Log("ARG " + i + ": " + args[i]);
            if (args[i] == "-folderInput") {
                input = args[i + 1];
            }
        }

    }
	
	// Update is called once per frame
	void Update () {
		
	}
}
