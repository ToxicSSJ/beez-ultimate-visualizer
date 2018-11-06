using System;
using System.IO;
using System.Collections;

using UnityEngine;
using UnityEditor;

public class UCamera : MonoBehaviour {

    public static bool freeze = false;
    public static bool sfile = false;

    // Use this for initialization
    void Start () {
		
	}

    // Update is called once per frame
    void Update() {

        if (Input.GetKey(KeyCode.H)) {

            if (sfile)
                return;

            sfile = true;
            freeze = true;

            Cursor.lockState = CursorLockMode.None;

            StartCoroutine(CommonTask.ExecuteAfterTime(0.1f, () => {
                Cursor.lockState = CursorLockMode.Confined;
                Cursor.visible = true;
            }));

            StartCoroutine(CommonTask.ExecuteAfterTime(0.5f, () => {

                Cursor.lockState = CursorLockMode.Confined;
                string path = EditorUtility.OpenFilePanel("Overwrite with data", "", "data");

                if (path.Length != 0) {

                    FileInfo file = new FileInfo(path);
                    StreamReader reader = file.OpenText();
                    string text = "";

                    while(text != null) {

                        text = reader.ReadLine();
                        Projector.coords.Add(text);

                    }

                    Projector.createBees();
                    Projector.createGameObjects();

                }

                freeze = false;

            }));

        }

    }
}
