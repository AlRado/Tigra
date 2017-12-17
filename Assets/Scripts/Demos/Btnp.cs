#region
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

#endregion

public class Btnp : Tic80 {

  private int x;
  private int y;

  public override void init () {
    x = 120;
    y = 80;
    cls (12);
  }

  public override void TIC () {
    if (btnp (0, 60, 6)) y -= 10;
    if (btnp (1, 60, 6)) y += 10;
    if (btnp (2, 60, 6)) x -= 10;
    if (btnp (3, 60, 6)) x += 10;

    rect (x, y, 10, 10, 8);
  }

}