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
class Tc6:Z{F v=0,hz=1,step,d=1.05f;void TIC(){cls();if(btn(0))hz*=d;if(btn(1))hz/=d;v%=240/hz;v+=4;if(hz<1)step=1;else if(hz>50)step=.02f;else step=1/hz;for(F x=-v;x<240;x+=step){F y=68+40*M.Cos(x*M.PI/120*hz);pix(x+v,y,3);}
print(M.Log(hz)/M.Log(d),1,127);line(0,67,240,67,3);}}
// original demo by kingdom5500
