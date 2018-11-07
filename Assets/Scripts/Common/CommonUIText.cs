using System.Collections;
using System.Collections.Generic;

using UnityEngine;
using UnityEngine.UI;

public class CommonUIText {

    public static void changeDistanceText(string text) {

        Text txt = GameObject.Find("distance").GetComponent<Text>();
        txt.text = text;

    }

    public static void changeBee1Text(string text) {

        Text txt = GameObject.Find("bee1").GetComponent<Text>();
        txt.text = text;

    }

    public static void changeBee2Text(string text) {

        Text txt = GameObject.Find("bee2").GetComponent<Text>();
        txt.text = text;

    }

}
