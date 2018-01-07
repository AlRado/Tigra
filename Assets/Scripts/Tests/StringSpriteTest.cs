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
  int x = 96, y = 44, s = 24, a = 14;

  void init () {
    // DrawSprite (x, y, a, DumpSprite(1), 3); 
  }

  void TIC () {
    cls (5);
    if (btn (0)) y--;
    if (btn (1)) y++;
    if (btn (2)) x--;
    if (btn (3)) x++;

    DrawSprite (x, y, a, "࿿ǿ￸／࿿ǿ￸￿∢Ģ裸袈！∢Ģ￯￿", 3);
    DrawSprite (x + s, y, a, "༁࿸ď༏༁࿸ď／∁༨Ǯ袈∁ＢǮ￿", 3);
    DrawSprite (x, y + s, a, "￿ǯÿ／∢Ģ裸袈辈ǿ裸裸袈ƈ￸￿", 3);
    DrawSprite (x + s, y + s, a, "！Ǯༀ∁ＢǮ袈蠁ྈǮ裸蠁ྈǾ＀", 3);
  }
}