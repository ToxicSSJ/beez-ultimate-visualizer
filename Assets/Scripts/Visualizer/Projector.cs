using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projector : MonoBehaviour {

    // 1587219.78885326,176236.116758158,-6155646.37437928

    public static List<string> coords = new List<string>();

    public static List<CommonBee> bees = new List<CommonBee>();
    public static List<GameObject> objs = new List<GameObject>();

    public static float minus_xoffset = -1500000;
    public static float minus_yoffset = -170000;
    public static float minus_zoffset = 6150000;

    public static void createBees() {

        bees.Clear();

        foreach(string text in coords) {

            if (text == null)
                continue;

            string[] coord = text.Split(new Char[] { ',' });

            double x = double.Parse(coord[0], System.Globalization.CultureInfo.InvariantCulture);
            double y = double.Parse(coord[1], System.Globalization.CultureInfo.InvariantCulture);
            double z = double.Parse(coord[2], System.Globalization.CultureInfo.InvariantCulture);

            bees.Add(new CommonBee(x, y, z));

        }

        print("[DEBUG] Cantidad de coordenadas pre-procesadas: " + bees.Count);

    }

    public static void createGameObjects() {

        foreach(GameObject obj in objs)
            Destroy(obj);

        objs.Clear();

        GameObject reference = GameObject.FindWithTag("Bee");
        MeshRenderer render;
        int count = bees.Count;

        foreach (CommonBee bee in bees) {

            double[] xyz = bee.toCartesianPoint3D();
            GameObject obj = Instantiate(reference);

            render = obj.GetComponentInChildren<MeshRenderer>();
            render.enabled = true;

            obj.transform.position = new Vector3(
                                         ((float) xyz[0] + minus_xoffset) / 300,
                                         ((float) xyz[1] + minus_yoffset) / 300,
                                         ((float) xyz[2] + minus_zoffset) / 100);

            obj.SetActive(true);
            objs.Add(obj);

        }

        if(objs.Count > 0) {

            GameObject obj = objs[0];
            Camera.main.transform.position = obj.transform.position;

        }

        print("[DEBUG] Cantidad de abejas pre-procesadas: " + bees.Count);

    }

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		


	}

}
