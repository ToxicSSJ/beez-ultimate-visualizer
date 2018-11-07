using System;
using System.IO;
using System.Collections;
using System.Collections.Generic;

using UnityEngine;
using UnityEditor;

public class UCamera : MonoBehaviour {

    public static bool freeze = false;
    public static bool sfile = false;

    private static int index = 0;
    
    void Start () {
        Projector.setDefaults();
    }
    
    void Update() {

        if (Input.GetKey(KeyCode.K) || Input.GetKey(KeyCode.LeftArrow)) {

            CommonUIButtons.pulseBack();
            GameObject cobj = null;

            if ((index - 1) < 0) {

                index = Projector.objs.Count - 1;
                cobj = Projector.objs[index];

                transform.position = cobj.transform.position;
                return;

            }

            --index;
            cobj = Projector.objs[index];

            transform.position = cobj.transform.position;
            return;

        }

        if (Input.GetKey(KeyCode.L) || Input.GetKey(KeyCode.RightArrow)) {

            CommonUIButtons.pulseNext();
            GameObject cobj;

            if ((index + 1) > (Projector.objs.Count - 1)) {

                index = 0;
                cobj = Projector.objs[index];

                transform.position = cobj.transform.position;
                return;

            }

            ++index;
            cobj = Projector.objs[index];

            transform.position = cobj.transform.position;
            return;

        }

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

                Cursor.lockState = CursorLockMode.None;

                StartCoroutine(CommonTask.ExecuteAfterTime(0.1f, () => {
                    Cursor.lockState = CursorLockMode.Locked;
                    Cursor.visible = false;
                }));

                freeze = false;

            }));

        }

    }
}
