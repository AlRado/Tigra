#region
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
#endregion

// title:  Tigra v.0.0.1
// author: Author
// desc:   Date: 12/19/2017 8:20:01 AM
// script: C#

public class SpritePrintTest : Tic80 {

  private int x;
  private int y;

  // Use this for initialization
  public override void init () {
    x = 1;
    y = 0;
  }

  // TIC is called 60 times every second
  public override void TIC () {
    if (btn (0)) y--;
    if (btn (1)) y++;
    if (btn (2)) x--;
    if (btn (3)) x++;

    cls (13);
    // print("Hello world!Hello world!Hello world! 1234567890", x, y);
    // print("Hello world!Hello world!Hello world! 1234567890", x, y+20, 5, true, 1);
    print("Hello world!", x, y, 5, false, 1);
    print("Hello world!", x, y+6, 5, true, 1);
    print("Hello world!", x, y+12, 6, scale: 2);
    print("Hello world!", x, y+24, 7, scale: 3);
    print("Hello world!", x, y+48, 8, scale: 4);
   
  }
}