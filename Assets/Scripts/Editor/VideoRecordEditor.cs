using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEditor;
using UnityEngine;

[CustomEditor (typeof (VideoRecord))]
public class VideoRecordEditor : Editor {
  public override void OnInspectorGUI () {
    DrawDefaultInspector ();

    VideoRecord videoRecord = (VideoRecord) target;
    if (GUILayout.Button ("Record Movie")) {
      if (Application.isPlaying) {
        videoRecord.RecordMovie ();
      } else {
        Debug.LogError ("You can only record Movie in runtime");
      }
    }
  }
}