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
class Tc3:Z{void TIC(){cls(2);var m=mouse();var x=m[0];var y=m[1];var p=m[2];circ(60,50,40,15);circ(180,50,40,15);circ(M.Min(80,M.Max(40,x)),M.Min(70,M.Max(30,y)),15,0);circ(M.Min(200,M.Max(160,x)),M.Min(70,M.Max(30,y)),15,0);}}
// original demo by lb_ii




