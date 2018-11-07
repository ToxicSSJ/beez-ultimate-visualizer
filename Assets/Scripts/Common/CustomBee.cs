using System.Linq;
using System.Collections;
using System.Collections.Generic;

using UnityEngine;

public class CustomBee : MonoBehaviour {

    public static LinkedList<CustomBee> selected = new LinkedList<CustomBee>();
    public const float drawspeed = 12f;

    private LinkedList<Renderer> renderers = new LinkedList<Renderer>();

    private GameObject obj;
    private GameObject sphere;

    private Renderer renderer;
    private Renderer srenderer;
    private LineRenderer lrenderer;

    private Transform destination;
    private Vector3 vector;

    private float distance = 0f;
    private float counter = 0f;

    void Start () {

        obj = this.gameObject;
        vector = Projector.lastvector;

        transform.localEulerAngles = new Vector3(Random.Range(0f, 180f),
                                                 Random.Range(0f, 180f),
                                                 Random.Range(0f, 180f));

        renderer = GetComponent<Renderer>();
        sphere = transform.GetChild(2).gameObject;

        srenderer = sphere.GetComponent<Renderer>();
        srenderer.material = Projector.radius;

        lrenderer = GetComponent<LineRenderer>();
        srenderer.enabled = false;

        lrenderer.SetPosition(0, transform.position);
        lrenderer.SetWidth(.45f, .45f);
        lrenderer.enabled = false;

        foreach (Transform child in transform)
            foreach (Transform sub in child) {

                GameObject obj = sub.gameObject;

                if (!obj.name.Contains("obj"))
                    continue;

                Renderer render = obj.GetComponent<Renderer>();
                renderers.AddFirst(render);

            }

    }

    public void setLLE(Vector3 vector) {

        this.vector = vector;
        return;

    }

    public void changeSphere(bool sphere) {
        srenderer.enabled = sphere;
    }

    public GameObject getGameObject() {
        return obj;
    }

    public void overBee() {

        renderer.material = Projector.wiremap;

        foreach (Renderer child in renderers)
            child.material = Projector.wiremap;

    }

    public void selectBee() {

        renderer.material = Projector.selected;

        foreach (Renderer child in renderers)
            child.material = Projector.selected;

    }

    public void unselectBee() {

        lrenderer.enabled = false;
        renderer.material = Projector.bee;

        foreach (Renderer child in renderers)
            child.material = Projector.wings;

    }

    void OnMouseDown() {

        if (selected.Contains(this) || selected.Count > 1) {

            if(selected.Count == 2) {

                foreach (CustomBee bee in selected) {

                    bee.changeSphere(false);
                    bee.unselectBee();

                }

                CommonUIText.changeBee1Text("Bee 1: [Unknow]");
                CommonUIText.changeBee2Text("Bee 2: [Unknow]");

                selected.Clear();
                return;

            }

            selected.Clear();
            CommonUIText.changeBee1Text("Bee 1: [Unknow]");

            changeSphere(false);
            unselectBee();
            return;

        }

        if (selected.Count == 0) {

            CommonUIText.changeBee1Text("Bee 1: [" + vector.x + ", " + vector.y + ", " + vector.z + "]");

            selected.AddFirst(this);
            selectBee();
            return;

        }

        CommonUIText.changeBee2Text("Bee 2: [" + vector.x + ", " + vector.y + ", " + vector.z + "]");

        selected.AddFirst(this);
        selectBee();

        CustomBee other = null;

        foreach (CustomBee bee in selected) {

            if (bee != this)
                other = bee;

            bee.changeSphere(true);

        }

        distance = Vector3.Distance(transform.position, other.getGameObject().transform.position);
        destination = other.getGameObject().transform;

        CommonUIText.changeDistanceText("Distance: " + distance + " meters");

        lrenderer.enabled = true;
        lrenderer.SetPosition(1, transform.position);
        lrenderer.SetPosition(1, other.getGameObject().transform.position);

    }

    void OnMouseOver() {

        if (selected.Contains(this))
            return;

        overBee();

    }

    void OnMouseExit() {

        if (selected.Contains(this))
            return;

        unselectBee();

    }

}
