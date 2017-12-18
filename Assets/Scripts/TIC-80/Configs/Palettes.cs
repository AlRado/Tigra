using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Palettes : MonoBehaviour {

  public enum Palette {
    DB16,
    PICO8,
    ARNE16,
    EDG16,
    A64,
    C64,
    VIC20,
    CGA,
    SLIFE,
    JMP,
    CGARNE,
    PSYG,
    EROGE,
    EISLAND
  }

  public static Dictionary<Palette, List<Color32>> COLORS = new Dictionary<Palette, List<Color32>> () { 
    { Palette.DB16, parsePalette ("140c1c44243430346d4e4a4e854c30346524d04648757161597dced27d2c8595a16daa2cd2aa996dc2cadad45edeeed6") }, 
    { Palette.PICO8, parsePalette ("0000007e25531d2b535f574fab5236008751ff004d83769cff77a8ffa300c2c3c700e756ffccaa29adfffff024fff1e8") }, 
    { Palette.ARNE16, parsePalette ("0000001b2632005784493c2ba4642244891abe26332f484e31a2f2eb89319d9d9da3ce27e06f8bb2dceff7e26bffffff") }, 
    { Palette.EDG16, parsePalette ("193d3f3f2832743f399e2835b86f50327345e53b444f67810484d1fb922bafbfd263c64de4a6722ce8f4ffe762ffffff") }, 
    { Palette.A64, parsePalette ("0000004c3435313a9148545492562b509450b148638080787655a28385cf9cabb19ccc47cd93738fbfd5bbc840ede6c8") }, 
    { Palette.C64, parsePalette ("00000057420040318d5050508b542955a0498839327878788b3f967869c49f9f9f94e089b8696267b6bdbfce72ffffff") }, 
    { Palette.VIC20, parsePalette ("000000772d2642348ba85fb4b668627e70caa8734a559e4ae99df5e9b287bdcc7185d4dc92df87c5ffffffffb0ffffff") }, 
    { Palette.CGA, parsePalette ("000000aa00000000aa555555aa550000aa00ff5555aaaaaa5555ffaa00aa00aaaa55ff55ff55ff55ffffffff55ffffff") }, 
    { Palette.SLIFE, parsePalette ("0000001226153f28117a2222513155d13b27286fb85d853acc8218e07f8a9b8bff68c127c7b581b3e868a8e4d4ffffff") }, 
    { Palette.JMP, parsePalette ("000000191028833129453e78216c4bdc534b7664fed365c846af45e18d79afaab9d6b97b9ec2e8a1d685e9d8a1f5f4eb") }, 
    { Palette.CGARNE, parsePalette ("deeed6dad45e6dc2cad2aa996daa2c8595a1d27d2c597dce757161d04648346524854c304e4a4e30346d442434140c1c") }, 
    { Palette.PSYG, parsePalette ("0000001b1e29003308362747084a3c443f41a2324e52524c546a0073615064647c516cbf77785be08b799ea4a7cbe8f7") }, 
    { Palette.EROGE, parsePalette ("0d080d2a23494f2b247d384032535f825b314180a0c16c5bc591547bb24e74adbbe89973bebbb2f0bd77fbdf9bfff9e4") }, 
    { Palette.EISLAND, parsePalette ("051625794765686086567864ca657e8686918184abcc8d867ea78839d4b98dbcd29dc085edc38de6d1d1f5e17af6f6bf") },
  };

  private static List<Color32> parsePalette (string palette) {
    var count = palette.Length / 6;
    var colorList = new List<Color32> ();
    for (int i = 0; i < count; i++) {
      colorList.Add (HexToColor (palette.Substring (i * 6, 6)));
    }

    return colorList;
  }

  private static Color32 HexToColor (string hex) {
    var r = byte.Parse (hex.Substring (0, 2), System.Globalization.NumberStyles.HexNumber);
    var g = byte.Parse (hex.Substring (2, 2), System.Globalization.NumberStyles.HexNumber);
    var b = byte.Parse (hex.Substring (4, 2), System.Globalization.NumberStyles.HexNumber);
    return new Color32 (r, g, b, 255);
  }

  public static Color32 GetColor (int colorIx, Palette palType) {
    var palette = COLORS[palType];
    var newColorIx = Mathf.Abs(colorIx) % (COLORS.Count+2);

    return palette[newColorIx];
  }

  public static int GetColorIx (Color32 color, Palette palType) {
    var palette = COLORS[palType];

    return palette.FindIndex (c => Object.Equals (c, color));
  }

}