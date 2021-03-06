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
class Tc8:Z{I x=96,y=44,s=24,a=14;void TIC(){cls(6);if(btn(0))y--;if(btn(1))y++;if(btn(2))x--;if(btn(3))x++;DS(x,y,a,"પ 䐄䑄䑄ꏮ䑄ꫮ",3);DS(x+s,y,a,"㺪䌄䍄㹄ꍃꋪ",3);DS(x,y+s,a,"㹄껿￿袈㻤㢈ｄ㢨ｄ㫺㻤䓤",3);DS(x+s,y+s,a,"䍄껿껿㺈㺈㻤ﾊ䏴ﾯ䋴㻤",3);}}


