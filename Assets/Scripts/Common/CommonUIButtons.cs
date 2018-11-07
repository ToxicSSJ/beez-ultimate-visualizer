using System;
using System.IO;
using System.Collections;
using System.Collections.Generic;

using UnityEngine;
using UnityEngine.UI;

public class CommonUIButtons : MonoBehaviour {

    private static bool busyNext = false, busyBack = false;
    private static CommonUIButtons instance;

    private static Color ccolor;

    void Start() {
        instance = this;
    }

	public static void pulseNext() {

        if (busyNext || busyBack)
            return;

        busyNext = true;

        Text txt = GameObject.Find("nextbee").GetComponent<Text>();
        ccolor = txt.color;

        txt.color = new Color(111f, 0f, 255f, 255f);
        instance.StartCoroutine(instance.ExecuteAfterTime(0.05f, () => {

            Text ctxt = GameObject.Find("nextbee").GetComponent<Text>();

            busyNext = false;
            ctxt.color = ccolor;

        }));

    }

    public static void pulseBack() {

        if (busyNext || busyBack)
            return;

        busyBack = true;

        Text txt = GameObject.Find("backbee").GetComponent<Text>();
        ccolor = txt.color;

        txt.color = new Color(111f, 0f, 255f, 255f);
        instance.StartCoroutine(instance.ExecuteAfterTime(0.05f, () => {

            busyBack = false;
            txt.color = ccolor;

        }));

    }

    IEnumerator ExecuteAfterTime(float time, Action task) {

        yield return new WaitForSeconds(time);
        task();

    }

}
