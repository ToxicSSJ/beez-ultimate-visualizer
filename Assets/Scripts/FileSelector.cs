using System.IO;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class FileSelector : EditorWindow {

    public const bool testf = true;

    [MenuItem("Example/Overwrite Texture")]
    public static void Apply() {

        string path = EditorUtility.OpenFilePanel("Overwrite with png", "", "png");
        Debug.Log(path);

    }

}
