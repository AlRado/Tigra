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
#endregion

  /**
  * Tigra shortcuts:
  *
  * Functions:
  * TIC() is called 60 times per second.
  *
  * Variables:
  * t:  elapsed time in seconds.
  *
  * Aliases:
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

class A:Z{F a,b;I[]d={2,3,4,4};V2[]c=new V2[4];void TIC(){if(a==0)c[3]=new V2(96,24);if(a++%10==0){cls();b+=R.Range(-1,1)*M.PI/4;int i;for(i=0;i<3;i++)c[i]=c[i+1];c[3].x+=M.Sin(b)*4;c[3].y+=M.Cos(b)*4;CD f=circb;for(i=0;i<4;i++){if(i==3)f=circ;f(c[i].x,c[i].y,d[i],6);}}}}
