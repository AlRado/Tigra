#region
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
#endregion

class SpriteTest : Tic80 {

  [Range(0,3)]
  public int Flip;
  [Range(0,3)]
  public int Rotate;

  int x = 96;
  int y = 24;

  void TIC () {
    if (btn (0)) y--;
    if (btn (1)) y++;
    if (btn (2)) x--;
    if (btn (3)) x++;

    cls (13);
    spr (1 + f % 60 / 30 * 2, x, y, 14, 3, Flip, Rotate, 2, 2);
    print ("HELLO WORLD!", 84, 84);
  }
}