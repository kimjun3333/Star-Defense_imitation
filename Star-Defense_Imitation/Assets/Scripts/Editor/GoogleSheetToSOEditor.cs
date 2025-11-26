using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class GoogleSheetToSOEditor : EditorWindow
{
    [MenuItem("Tools/Google Sheet => SO 생성기")]
    public static void OpenWindow()
    {
        GetWindow<GoogleSheetToSOEditor>("Google Sheet Importer");
    }


}
