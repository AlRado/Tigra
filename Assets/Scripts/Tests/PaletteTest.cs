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
    // reduced 2 color palette
    byte[] data8 = { 11, 22, 13, 124, 5, 6, 187, 8 };
    var dataStr = Convert.ToBase64String (data8);
    // added indexes of colors
    dataStr = "1a;"+dataStr;
    Debug.LogError ("1 bit, 2 colors, 8 bytes per sprite:  " + dataStr + ", Length: " + dataStr.Length);

    // reduced 4 color palette
    byte[] data16 = { 11, 2, 13, 255, 5, 16, 73, 88,
                      11, 22, 13, 124, 5, 6, 187, 8 };
    dataStr = Convert.ToBase64String (data16);
    // added indexes of colors
    dataStr = "12ab;"+dataStr;
    Debug.LogError ("2 bit, 4 colors, 16 bytes per sprite: " + dataStr + ", Length: " + dataStr.Length);

    // reduced 8 color palette
    byte[] data24 = { 11, 22, 13, 124, 5, 6, 187, 8,
                      11, 2, 13, 255, 5, 16, 73, 88,
                      11, 22, 13, 124, 5, 6, 187, 8 };
    dataStr = Convert.ToBase64String (data24);
    // added indexes of colors
    dataStr = "1234abcd;"+dataStr;
    Debug.LogError ("3 bit, 8 colors, 24 bytes per sprite: " + dataStr + ", Length: " + dataStr.Length);

    // 16 color palette (all colors)
    byte[] data32 = { 11, 22, 13, 124, 5, 6, 187, 8,
                      11, 2, 13, 255, 5, 16, 73, 88,
                      11, 22, 13, 124, 5, 6, 187, 8,
                      11, 2, 13, 255, 5, 16, 73, 88, };
    dataStr = Convert.ToBase64String (data32);
    Debug.LogError ("4 bit, 16 colors, 32 bytes per sprite: " + dataStr + ", Length: " + dataStr.Length);
  }

}