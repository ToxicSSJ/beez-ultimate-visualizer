using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CommonTask : MonoBehaviour {

    public static IEnumerator ExecuteAfterTime(float time, Action task) {
        
        yield return new WaitForSeconds(time);
        task();

    }

}
