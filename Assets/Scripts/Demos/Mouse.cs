#region
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

#endregion

public class Mouse : Tic80 {

  // title:  mouse demo
  // author: Filippo
  // desc:   
  // script: js
  // input:  mouse

  private int r;

  public override void init () {
    r = 0;
  }

  public override void TIC () {
    cls (0);

    //get mouse info
    var m = mouse ();
    var x = m[0];
    var y = m[1];
    var p = m[2];

    //if pressed
    if (p == 1) r = r + 2;
    r = r - 1;
    r = Mathf.Max (0, Mathf.Min (32, r));

    //draw stuff
    line (x, 0, x, 136, 11);
    line (0, y, 240, y, 11);
    circ (x, y, r, 11);

    //show coordinates
    trace("(" + x + ", " + y + ")");
  }
}