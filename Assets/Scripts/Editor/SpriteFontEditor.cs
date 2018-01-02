using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEditor;
using UnityEngine;

[CustomEditor (typeof (SpriteFont))]
public class SpriteFontEditor : Editor {
  public override void OnInspectorGUI () {
    DrawDefaultInspector ();

    SpriteFont spriteFont = (SpriteFont) target;

    foreach (var font in spriteFont.Fonts) {
      font.Name = font.Type.ToString ();
    }

    if (GUILayout.Button ("Add Font")) {
      spriteFont.AddFont ();
    }
  }
}