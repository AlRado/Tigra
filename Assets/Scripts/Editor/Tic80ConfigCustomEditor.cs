using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEditor;
using UnityEngine;

[CustomEditor (typeof (Tic80Config))]
public class Tic80ConfigCustomEditor : Editor {
  public override void OnInspectorGUI () {
    DrawDefaultInspector ();

    Tic80Config tic80Config = (Tic80Config) target;
    Tic80 activeComponent = tic80Config.GetComponents<Tic80> ().ToList ().Find (x => x.enabled);
    if (GUILayout.Button ("Make Cover")) {
      if (Application.isPlaying) {
        activeComponent.SaveScreenshot ();
      } else {
        Debug.LogError ("The cover can only be produce in runtime");
      }
    }
  }
}