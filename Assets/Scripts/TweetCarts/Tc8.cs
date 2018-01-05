#region
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Text;
using Moments.Encoder;
using UnityEngine;
using Z = Tic80;
using M = UnityEngine.Mathf;
using R = UnityEngine.Random;
using V2 = UnityEngine.Vector2;
using V3 = UnityEngine.Vector3;
using F = System.Single;
using I = System.Int32;
using S = System.String;
using B = System.Boolean;
#endregion

/**
 * Tweet cart is the whole script, fit in one tweet, i.e. code length must be <= 280 characters
 *
 * Tigra tweet cart shortcuts:
 *
 * Functions:
 * TIC() is called 60 times per second.
 *
 * Variables:
 * t:  elapsed time in seconds
 * f:  frame counter
 *
 * Aliases:
 * B:  bool
 * F:  float
 * I:  int
 * M:  UnityEngine.Mathf
 * R:  UnityEngine.Random
 * S:  System.String
 * V2: UnityEngine.Vector2
 * V3: UnityEngine.Vector3
 * Z:  Tic80
 *
 * Delegates:
 * CD: circ & circb delegate
 * RD: rect & rectb delegate
 * TD: tri & trib delegate
 *
 * Beautify/minify C# online tool:
 * https://codebeautify.org/csharpviewer
 */
class Tc8 : Z {
  I x,y;I s=24,a=14;
  
  void TIC () {
    if(btn(0))y--;
    if (btn (1)) y++;
    if (btn (2)) x--;
    if (btn (3)) x++;
    DS (x, y, a, "Ęğ/ğĘğ/ğĘğ/ğĘğğğĘBBBĘ¨¨¨ğBBBďğğğ",3);
    DS (x+s, y, a, "/Ę///Ę///Ę//ğĘ/ğBH/Ď¨¨/ĎBBğĎğğĞĎ",3);
    DS (x, y+s, a, "ďğğďğ /ğĒBBBĘ¨¨¨Ę¨¯ğĘ¨Ę¨Ę¨¨¨Ęğğğ",3);
    DS (x+s, y+s, a, "ğğĎĎ /ĞĎBBğĎ¨¨/Ď¨¨/ĎĘ¨/Ď¨¨/ĞğĘ ğ",3);
  }
}


// class Tc8:Z{I x,y;I s=24,a=14;void TIC(){if(btn(0))y--;if(btn(1))y++;if(btn(2))x--;if(btn(3))x++;DS(x,y,a,"Ęğ/ğĘğ/ğĘğ/ğĘğğğĘBBBĘ¨¨¨ğBBBďğğğ",3);DS(x+s,y,a,"/Ę///Ę///Ę//ğĘ/ğBH/Ď¨¨/ĎBBğĎğğĞĎ",3);DS(x,y+s,a,"ďğğďğ /ğĒBBBĘ¨¨¨Ę¨¯ğĘ¨Ę¨Ę¨¨¨Ęğğğ",3);DS(x+s,y+s,a,"ğğĎĎ /ĞĎBBğĎ¨¨/Ď¨¨/ĎĘ¨/Ď¨¨/ĞğĘ ğ",3);}}

