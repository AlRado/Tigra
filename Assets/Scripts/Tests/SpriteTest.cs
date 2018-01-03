#region
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
#endregion

// title:  #PROJECTNAME# v.#VERSION#
// author: #AUTHOR#
// desc:   Date: #CREATIONDATE#
// script: C#

class SpriteTest : Tic80 {

  int x = 96;
  int y = 24;  

  void TIC () {
    if (btn (0)) y--;
    if (btn (1)) y++;
    if (btn (2)) x--;
    if (btn (3)) x++;

    cls (13);
    spr (1 + f % 60 / 30 * 2, x, y, 14, 3, 0, 0, 2, 2);
    print ("HELLO WORLD!", 84, 84);
  }
}