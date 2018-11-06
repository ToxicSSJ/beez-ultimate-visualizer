using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CommonClone : MonoBehaviour {
    
	void Start () {
        MeshRenderer render = gameObject.GetComponentInChildren<MeshRenderer>();
        render.enabled = true;
    }

}
