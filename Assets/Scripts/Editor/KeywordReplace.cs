using System.Collections;
using UnityEditor;
using UnityEngine;

public class KeywordReplace : UnityEditor.AssetModificationProcessor {

  public static void OnWillCreateAsset (string path) {
    path = path.Replace (".meta", "");
    int index = path.LastIndexOf (".");
    if (index < 0) return;

    string file = path.Substring (index);
    if (file != ".cs" && file != ".js" && file != ".boo") return;

    index = Application.dataPath.LastIndexOf ("Assets");
    path = Application.dataPath.Substring (0, index) + path;
    if (!System.IO.File.Exists (path)) return;

    string fileContent = System.IO.File.ReadAllText (path);
    fileContent = fileContent.Replace ("#CREATIONDATE#", System.DateTime.Now + "");
    fileContent = fileContent.Replace ("#PROJECTNAME#", PlayerSettings.productName);
    fileContent = fileContent.Replace ("#VERSION#", PlayerSettings.bundleVersion);
    fileContent = fileContent.Replace ("#AUTHOR#", PlayerSettings.companyName);

    var startIx = path.LastIndexOf ("/")+1;
    var endIx = path.LastIndexOf (".");
    var len = endIx - startIx;
    var fileName = path.Substring (startIx, len);
    Debug.LogError ("fileName: " + fileName);

    var clipboard = GUIUtility.systemCopyBuffer;
    startIx = "class ".Length;
    endIx = clipboard.IndexOf (":");
    len = endIx - startIx;
    clipboard = clipboard.Remove(startIx, len);
    clipboard = clipboard.Insert(startIx, fileName);
    fileContent = fileContent.Replace ("#CLIPBOARD#", clipboard);

    System.IO.File.WriteAllText (path, fileContent);
    AssetDatabase.Refresh ();
  }
}