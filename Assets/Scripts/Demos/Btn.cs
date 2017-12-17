#region
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

#endregion

public class Btn : Tic80 {

  private int x;
  private int y;
  private List<string> btnLabel = new List<string> () { "Up", "Down", "Left", "Right", "Btn A", "Btn B" , "Btn X", "Btn Y" };

  public override void init () {
    x = 70;
    y = 25;
  }

  public override void TIC () {
    if (btn (0)) y--;
    if (btn (1)) y++;
    if (btn (2)) x--;
    if (btn (3)) x++;

    cls ();
    circ (x, y, 40, 6);

    for (var i = 0; i < 8; ++i) {
      if (btn (i)) trace (btnLabel[i]);
    }
  }

}