#region
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
#endregion

// title:  Tigra-128 v.0.2.0
// author: Author
// desc:   Date: 1/6/2018 12:59:39 AM
// script: C#

class PaletteTest : Tic80 {

  void init () {
    // DrawSprite (x, y, a, DumpSprite(1), 3); 
    byte[] data = { 11, 2, 13, 14, 5, 6, 7, 8, 9, 10, 1, 12, 3, 44, 25, 255 };
    var dataStr = Convert.ToBase64String (data);
    Debug.LogError (dataStr + ", dataStr.len: " + dataStr.Length);
  }

}