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
class Tc7:Z{S m="Hello world!";F o;I c;void init(){o=(240-m.Length*6)/2;}void TIC(){cls();for(F i=0;i<m.Length;i++){if(f%10==0)c++;c=c==0?1:c;print(m[(I)i],o+i*6,64+M.Sin(i/2+f/M.PI/4)*16,c);}}}
// original demo by josefnpat
