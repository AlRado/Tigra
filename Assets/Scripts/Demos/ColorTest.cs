#region
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

#endregion

public class ColorTest : Tic80 {

  private int x;
  private int y;
  private int column_width;

  public override void init () {
    x = 65;
    y = 65;
    column_width = 240/16;
  }

  public override void TIC () {
    if (btn (0)) y--;
    if (btn (1)) y++;
    if (btn (2)) x--;
    if (btn (3)) x++;

    cls ();
    var color = ((x / 10) + 5) % 16;
    border (color);

    for (var i = 0; i < 16; ++i) {
      rect (i * column_width, 0, column_width, 136, i);
    }
  
    rect(x-2,y-2,116,10,15);
    print("TIGRA! HELLO WORLD!",x,y,color);

  }

}