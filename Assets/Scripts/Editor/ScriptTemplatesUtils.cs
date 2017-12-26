    using UnityEditor;
    using UnityEngine;

    public class ScriptTemplatesUtils {

      [MenuItem ("Assets/Show in explorer Tic80 Script Templates")]
      public static void CopyScriptTemplates () {
        var scriptTemplatesSrcPath = Application.dataPath + "/Scripts/Editor/ScriptTemplates/99-Tic80 Script-NewTic80Script.cs.txt";
        var scriptTemplatesDstPath = UnityEditor.EditorApplication.applicationContentsPath + "/Resources/ScriptTemplates/81-C# Script-NewBehaviourScript.cs.txt";
        EditorUtility.RevealInFinder(scriptTemplatesSrcPath);
        EditorUtility.RevealInFinder(scriptTemplatesDstPath);
      }

    }