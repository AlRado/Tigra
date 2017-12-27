#region
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
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
class Tc4:Z{F PI2=(F)2.0*M.PI;I dx=32,dy=32,ts=0;void TIC(){cls();if(dy<0)dy=0;if(dx<0)dx=0;for(F x=-dx;x<220+2*dx;x+=dx){for(F y=-dy;y<136+2*dy;y+=dy){for(F a=0;a<PI2;a+=PI2/8){circ(x+35*M.Sin(a+t),y+35*M.Cos(a+t),5,15);}}}}}
// original demo by HomineLudens
