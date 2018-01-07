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

class StringSpriteTest : Tic80 {
  int x = 96, y = 44, s = 24, a = -1;

  void init () {
    // DrawSprite (x, y, a, DumpSprite(1), 3); 

    StartCoroutine(drawRndSpriteCoroutine());
  }

  IEnumerator drawRndSpriteCoroutine () {
    string dumpedSpr;
    int count = 1000;
    for (int i = 0; i < count; i++) {
      dumpedSpr = DumpSpriteData(GetRndSprData());
      DrawSprite (x, y, a, dumpedSpr, 3);
      yield return 0;
    }
  }

  private byte[] GetRndSprData () {
    byte[] data = new byte[64];
    var count = 64 * 64;
    for (int i = 0; i < count; i++) {
      var val = UnityEngine.Random.RandomRange (0, 16);
      var pos = UnityEngine.Random.RandomRange (0, data.Length);
      data[pos] = Convert.ToByte(val);
    }
    return data;
  }

}