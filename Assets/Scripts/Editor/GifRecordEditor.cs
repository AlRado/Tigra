using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEditor;
using UnityEngine;

[CustomEditor (typeof (GifRecord))]
public class GifRecordEditor : Editor {
  public override void OnInspectorGUI () {
    DrawDefaultInspector ();

    GifRecord gifRecord = (GifRecord) target;
    if (GUILayout.Button ("Save Gif")) {
      if (Application.isPlaying) {
        gifRecord.SaveGif ();
      } else {
        Debug.LogError ("You can only save Gif in runtime");
      }
    }
  }
}