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
class Tc5:Z{F x=96,y=24,d=.01f;void TIC(){cls(16);if(btn(0))y-=d;if(btn(1))y+=d;if(btn(2))x-=d;if(btn(3))x+=d;for(I e=0;e<136;e+=2){for(I b=0;b<240;b+=50){rect(b,e,M.Sin((y*(e*b)/x)+(F)f/200)*50,1,R.Range(6,10));}}}}
// original demo by catpants