using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fonts : MonoBehaviour {

  public enum FontType {
    WIDE,
    REGULAR
  }

  public static Dictionary<FontType, Font> FONTS = new Dictionary<FontType, Font> () { 
    { FontType.WIDE, loadFont ("Fonts/TIC-80 wide") }, 
    { FontType.REGULAR, loadFont ("Fonts/TIC-80 regular") }, 
  };

  private static Font loadFont (string path) {
    return Resources.Load<Font> (path);
  }


  public static Font GetFont (FontType type) {
    Font font = null;
    FONTS.TryGetValue(type, out font);

    return font;
  }

}