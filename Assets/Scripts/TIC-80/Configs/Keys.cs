using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Keys {
  // TODO переделать этот хардкод
  public static List<KeyCode> KEYS = new List<KeyCode> () {
    KeyCode.UpArrow,
    KeyCode.DownArrow,
    KeyCode.LeftArrow,
    KeyCode.RightArrow,

    KeyCode.Z,
    KeyCode.X,
    KeyCode.A,
    KeyCode.S,
  };

  public static KeyCode GetKey (int ix) {
    if (ix < 0 || ix >= KEYS.Count) return KeyCode.None;

    return KEYS[ix];
  }
}